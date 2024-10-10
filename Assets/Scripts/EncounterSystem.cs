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

[DefaultExecutionOrder(1)]
public class EncounterSystem : MonoBehaviour
{



    
    
    
    //Personal
    
    [SerializeField]
    private List<EncounterArea> _areas = new List<EncounterArea>();
    [SerializeField] private int areaIndex = 0;

    [Header("Ships")]
    public GameObject Player;
    private Rigidbody2D _playerRb;
    public GameObject _enemy;
    
    [Header("Canvas")]
    [SerializeField] Canvas BattleUICanvas;


    [Header("Encounter Information")]
    [SerializeField] private bool inCombat = false;
    [SerializeField] private float distanceTravelledSinceLastEncounter;
    [SerializeField] private float distanceTraveled;
    [SerializeField][Range(1f,10000f)] private float minEncounterDistance = 5;
    [SerializeField] private Ship firstTurn;
    public EncounterArea currentArea;
    
    public static EncounterSystem Instance{ get; private set; }


    public UnityEvent<Ship> onEnterCombat;
    public UnityEvent onExitCombat;


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
        #endregion
        
        #region Get RigidBody From Player
        
        Player = GameObject.FindGameObjectWithTag("Player");
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
        if (_enemy !=null) Debug.Log(_enemy.GetComponent<Ship>().health);
        
        if (_playerRb.velocity.magnitude > 5)
        {
            distanceTravelledSinceLastEncounter += _playerRb.velocity.magnitude * Time.deltaTime;
            distanceTraveled += _playerRb.velocity.magnitude * Time.deltaTime;
            if (distanceTravelledSinceLastEncounter >= minEncounterDistance)
            {
                distanceTravelledSinceLastEncounter = 0;
                if (RollEncounter())
                {
                    Ship enemyShip = GenerateShip();
                    EnterEncounter(enemyShip);
                }
                else Debug.Log("Failed to enter Encounter");
            }
        }
    }

    private Ship GenerateShip()
    {
        int randomIndex = Random.Range(0, currentArea.areaStats.enemyShips.Length);
        _enemy = Instantiate(currentArea.areaStats.enemyShips[randomIndex]);
        
        
        return _enemy.GetComponent<Ship>();
    }



    private void EnterEncounter(Ship EnemyShip = null)
    {
        inCombat = true;
        Player.SetActive(!inCombat);
        BattleUICanvas.gameObject.SetActive(inCombat);
        onEnterCombat?.Invoke(EnemyShip);
    }


    private bool RollEncounter()
    {
        if (_areas[areaIndex].IsUnityNull()) return false;

        return _areas[areaIndex].RollEncounter();
        // return _areas.Peek().RollEncounter();
    }
    

    public void ActivateWeapon(Ship caster,Ship target,BaseWeapon weapon)
    {
        if (caster != null && target != null)
        {
            DamageValues damageApplied = weapon.ApplyDamage(caster,target);
        }
    }
    

    public void FleeBattleScene()
    {
        onExitCombat?.Invoke();
        inCombat = false;
        Player.SetActive(!inCombat);
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
    private void EnterEncounteContextMenu()
    {
        EnterEncounter(GenerateShip());
    }
}
