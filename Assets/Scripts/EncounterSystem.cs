using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using AI.MINIMAX;

[DefaultExecutionOrder(1)]
public class EncounterSystem : MonoBehaviour
{



    
    
    
    //Personal
    
    [SerializeField]
    private List<EncounterArea> _areas = new List<EncounterArea>();
    [SerializeField] private int areaIndex = 0;

    [Header("Ships")]
    public Ship Player;
    private Rigidbody2D _playerRb;
    public Ship Enemy;
    
    [Header("Canvas")]
    [SerializeField] Canvas BattleUICanvas;


    [Header("Encounter Information")]
    [SerializeField] private bool isDebugging = true;
    [SerializeField] public bool inCombat = false;
    [SerializeField] private float distanceTravelledSinceLastEncounter;
    [SerializeField] private float distanceTraveled;
    [SerializeField][Range(1f,10000f)] private float minEncounterDistance = 5;
    public EncounterArea currentArea;
    
    public static EncounterSystem Instance{ get; private set; }


    public UnityEvent<Ship> onEnterCombat;
    public UnityEvent onExitCombat;

    public EncounterState _currentState { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
    
    }


    // Start is called before the first frame update
    void Start()
    {
        
       
        #region Find canvas in other Scene
        GameObject[] rootGameObjects = SceneManager.GetSceneByName("Battle").GetRootGameObjects();
        foreach (var o in rootGameObjects)
        {
            if (o.CompareTag("BattleUI"))
            {
                BattleUICanvas = o.gameObject.GetComponent<Canvas>();
                BattleUICanvas.GameObject().SetActive(false);
                break;
            }
        }
        #endregion
        #region Get RigidBody From Player
        
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
        _playerRb = Player.GetComponent<Rigidbody2D>();

        #endregion

        #region Get All Areas from Scene

        _areas.AddRange(GameObject.FindWithTag("Areas").GetComponentsInChildren<EncounterArea>());
        _areas.Add(null);
        #endregion
        
        currentArea = _areas[areaIndex];
    }

    void Update()
    {
        
        if (_playerRb.velocity.magnitude > 5)
        {
            distanceTravelledSinceLastEncounter += _playerRb.velocity.magnitude * Time.deltaTime;
            distanceTraveled += _playerRb.velocity.magnitude * Time.deltaTime;
            if (distanceTravelledSinceLastEncounter >= minEncounterDistance)
            {
                distanceTravelledSinceLastEncounter = 0;
                if (RollEncounter())
                {
                    //Todo: Play animation 
                    
                    Ship enemyShip = GenerateEnemyShip();
                    EnterEncounter(enemyShip);
                }
                else Debug.Log("Failed to enter Encounter");
            }
        }
    }

    private Ship GenerateEnemyShip()
    {
        int randomIndex = Random.Range(0, currentArea.areaStats.enemyShips.Length);
        Enemy = Instantiate(currentArea.areaStats.enemyShips[randomIndex]).GetComponent<Ship>();
        Enemy.GetComponent<EnemyAI>().targetShip = Player;
        return Enemy;
    }

    /// <summary>
    /// Determines which ship will take the first turn by randomly selecting between the player ship and the enemy ship.
    /// Sets the <c>currentTurn</c> property of the selected ship to <c>true</c>.
    /// </summary>
    /// <returns>
    /// The ship that is chosen to take the first turn, either the player ship or the enemy ship.
    /// </returns>
    private void DecidedTurnOrder(Ship pShip,Ship eShip,out Ship attackingShip,out Ship defendingShip)
    {
        int randomINT = Random.Range(0, 2);
        if (randomINT == 0)
        {
            attackingShip = pShip;
            attackingShip.currentTurn = true;
            defendingShip = eShip;
            defendingShip.currentTurn = false;
        }
        else
        {
            attackingShip = eShip;
            attackingShip.currentTurn = true;
            defendingShip = pShip;
            defendingShip.currentTurn = false;
        }
    }




    private void EnterEncounter(Ship enemyShip = null)
    {
        inCombat = true;
        
        
        DecidedTurnOrder(Player,enemyShip,out Ship attackingShip,out Ship defendingShip);
        _currentState = new EncounterState(attackingShip,defendingShip);
        
        if (isDebugging) Debug.Log(_currentState.logOfActions[_currentState.TurnCount]);
        _currentState.TurnCount++;
        
        Player.GameObject().SetActive(!inCombat);
        BattleUICanvas.gameObject.SetActive(inCombat);
        onEnterCombat?.Invoke(enemyShip);
    }
    


    private bool RollEncounter()
    {
        if (_areas[areaIndex].IsUnityNull()) return false;

        return _areas[areaIndex].RollEncounter();
        // return _areas.Peek().RollEncounter();
    }
    

    public void ActivateWeapon(Ship caster,Ship target,BaseWeapon weapon)
    {
        if (!caster.IsUnityNull() && !target.IsUnityNull())
        {
            DamageValues damageApplied = caster.Attack(target,weapon);
            ActionLog log = new ActionLog(caster,target,weapon,damageApplied,_currentState.TurnCount);
            
            if (isDebugging) Debug.Log(log);
            
            _currentState.UpdateEncounterState(log);
            SetNextTurn();
        }
    }

    private void SetNextTurn()
    {
        _currentState.TurnCount++;
    }

    private void ShipDeath()
    {
        //todo:Create broadcast for what happens when  ship dies, I need to pass in which ship dies
    }
    
    
    public void FleeBattleScene()
    {
        onExitCombat?.Invoke();
        inCombat = false;
        Player.gameObject.SetActive(!inCombat);
        BattleUICanvas.gameObject.SetActive(inCombat);
    }



    public void EnteredArea(EncounterArea enteredArea)
    {
        areaIndex = _areas.IndexOf(enteredArea);
        currentArea = _areas[areaIndex];

    }

    public void ExitedArea(EncounterArea exitedArea)
    {
        areaIndex = _areas.IndexOf(exitedArea)+1;
        currentArea = _areas[areaIndex];
    }
    
    
    //###################################################################
    [ContextMenu("Enter Combat")]
    private void EnterEncounterContextMenu()
    {
        EnterEncounter(GenerateEnemyShip());
    }

    [ContextMenu("Generate Tree")]
    private void GenerateTreeContextMenu()
    {
        
        
        Tree<EncounterState> Tree = new Tree<EncounterState>(EncounterState.Clone(_currentState), true);

        
        Func<EncounterState, LinkedList<EncounterState>> getChildren = (state) => state.GetPossibleStates();

        Tree.GenerateTree(3,getChildren);
        Tree.PrintTree(Tree.Root);
    }


}
