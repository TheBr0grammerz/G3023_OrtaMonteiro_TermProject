using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public EnemyShipNames _enemyShipNames;
    
    [Header("Canvas")]
    [SerializeField] public Canvas BattleUICanvas;
    [SerializeField] public Animator BattleAnimator;


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

    [SerializeField] private float playerStartChance;

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

                break;
            }
        }
        
        BattleUICanvas.GameObject().SetActive(false);

        #endregion
        #region Get RigidBody From Player
        
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
        _playerRb = Player.GetComponent<Rigidbody2D>();

        #endregion

        #region Get All Areas from Scene

        _areas.AddRange(GameObject.FindWithTag("Areas").GetComponentsInChildren<EncounterArea>());
        _areas.Add(null);
        #endregion

        BattleAnimator = BattleUICanvas.GetComponent<Animator>();
        currentArea = _areas[areaIndex];
        _enemyShipNames.ImportNames();
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
                    Enemy = GenerateEnemyShip();
                    EnterEncounter(Enemy);
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

        //todo: This is a placeholder, we need to put in a way to generate health properly

        Enemy.maxHealth = Player.maxHealth;
        Enemy.health = Player.health;
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
        float randomChance = Random.Range(0f, 1f);
        if (randomChance <= playerStartChance)
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
        //inCombat = true;

        DecidedTurnOrder(Player,enemyShip,out Ship attackingShip,out Ship defendingShip);
        _currentState = new EncounterState(attackingShip, defendingShip);
       // _currentState = new EncounterState(Player,Enemy);
        
        if (isDebugging) Debug.Log(_currentState.logOfActions.Last());
        
        Player.GameObject().SetActive(false);
        BattleUICanvas.gameObject.SetActive(true);
        onEnterCombat?.Invoke(enemyShip);
    }

    public void TransitionToBattleCanvas()
    {
        
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

            UITextManager.SetAllText(log.Description);

            TriggerAttackAnim(caster, weapon);
            
            if (isDebugging) Debug.Log(log);
            
            _currentState.UpdateEncounterState(log);

            if(target.isDead) ShipDeath(caster,target);
        }
    }

    private void TriggerAttackAnim(Ship caster, BaseWeapon weapon)
    {
        BattleUICanvas.GetComponent<AnimationController>().SetProjectileImage(weapon.Icon);

        if (caster == Player)
            BattleAnimator.SetTrigger("PlayerAttack");
        else
            BattleAnimator.SetTrigger("EnemyAttack");
    }

    private void ShipDeath(Ship caster,Ship deadShip)
    {
        ActionLog log = new ActionLog();
        log.LogDeath(caster,deadShip);
        _currentState.logOfActions.Add(log);

        if(deadShip == Enemy)
        {
            Destroy(Enemy.GameObject());
        }
        else
        {
            Debug.Log("Player has died, Need to implement Game Over");
        }    
            onExitCombat?.Invoke();
            inCombat = false;
            Player.gameObject.SetActive(!inCombat);
            BattleUICanvas.gameObject.SetActive(inCombat);
    
    }
    
    public void FleeBattleScene()
    {
        ActionLog log = new ActionLog();
        log.LogFlee();
        _currentState.UpdateEncounterState(log);

        onExitCombat?.Invoke();
        inCombat = false;
        Player.gameObject.SetActive(!inCombat);
        BattleUICanvas.gameObject.SetActive(inCombat);
        Destroy(Enemy.gameObject); // todo: temp solution
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

}
