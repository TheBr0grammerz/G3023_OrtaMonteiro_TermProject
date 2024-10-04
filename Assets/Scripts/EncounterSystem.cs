using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EncounterSystem : MonoBehaviour
{
    //In Class
    
    public event Action<EncounterSystem,BattleCharacter,BattleCharacter,Effect> OnAttack;



    public void AttackCharacter(BattleCharacter caster, BattleCharacter target,int slotIdentifier)
    {
        Effect currentEffect = caster.GetEffectAtWeaponSlot(slotIdentifier);
        currentEffect.ApplyEffect();
        
        OnAttack?.Invoke(this, caster, target, currentEffect);
    }
    
    
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
    public EncounterArea currentEncounter;
    
    
    
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

        _playerRb = Player.GetComponent<Rigidbody2D>();

        #endregion

        #region Get All Areas from Scene

        _areas.AddRange(GameObject.FindWithTag("Areas").GetComponentsInChildren<EncounterArea>());
        _areas.Add(null);
        #endregion
        
        currentEncounter = _areas[areaIndex];
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
                    EnterEncounter();
                    //Debug.Log("Encounter Encountered Inside Area: ", _areas.Peek());
                }
                else Debug.Log("Failed to enter Encounter");
            }

        }
    }


    public bool RollEncounter()
    {
        if (_areas[areaIndex].IsUnityNull()) return false;

        return _areas[areaIndex].RollEncounter();
        // return _areas.Peek().RollEncounter();
    }
    

    public void ActivateAbility(int weaponSlot)
    {
       // Ability abilty = Player.GetComponent<ShipStats>().GetWeaponStorage(weaponSlot);



       // abilty.Activate();
    }
    

    public void FleeBattleScene()
    {
        inCombat = false;
        Player.SetActive(!inCombat);
        BattleUICanvas.gameObject.SetActive(inCombat);
    }

    public void EnterEncounter()
    {
        inCombat = true;
        Player.SetActive(!inCombat);
        BattleUICanvas.gameObject.SetActive(inCombat);
        
    }

    public void EnteredArea(EncounterArea enteredArea)
    {
        areaIndex = _areas.IndexOf(enteredArea);
        currentEncounter = _areas[areaIndex];

    }

    public void ExitedArea(EncounterArea exitedArea)
    {
        areaIndex = _areas.IndexOf(exitedArea)+1;
        currentEncounter = _areas[areaIndex];
        
    }
}
