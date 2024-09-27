using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterSystem : MonoBehaviour
{
    
    public Queue<EncounterArea> zones = new Queue<EncounterArea>();

    public GameObject Player;
    private GameObject _enemy;
    
    
    private Canvas BattleUICanvas;
    
    private bool inCombat = false;
    // Start is called before the first frame update
    void Start()
    {
       // var _zones = GameObject.FindWithTag("Areas").GetComponentsInChildren<EncounterArea>();
       // foreach (EncounterArea area in _zones)
       // {
       //     area.encounterSystem = this;
       //     zones.Push(area);
       // }
        
        
        
        
        GameObject[] rootGameObjects = SceneManager.GetSceneByName("Battle").GetRootGameObjects();
        foreach (var o in rootGameObjects)
        {
            if (o.CompareTag("BattleUI"))
            {
                BattleUICanvas = o.gameObject.GetComponent<Canvas>();
                break;
            }
        }
    }


    public bool RollEncounter()
    {
        if (zones == null) return false;

        return zones.Peek().RollEncounter();
    }
    
    public void ExitEncounter()
    {
        BattleUICanvas.gameObject.SetActive(false);

    }

    public void ActivateAbility(int weaponSlot)
    {
       // Ability abilty = Player.GetComponent<ShipStats>().GetWeaponStorage(weaponSlot);



       // abilty.Activate();
    }
    void BattleScene()
    {
        
    }

    void HideBattleScene()
    {
        
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
}
