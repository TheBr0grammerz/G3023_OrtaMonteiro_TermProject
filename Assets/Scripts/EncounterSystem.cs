using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterSystem : MonoBehaviour
{

    public GameObject Player;
    private GameObject _enemy;
    
    private EncounterArea area;
    private Canvas BattleUICanvas;
    
    private bool inCombat = false;
    // Start is called before the first frame update
    void Start()
    {
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

    
    
    public void ExitEncounter()
    {
        BattleUICanvas.gameObject.SetActive(false);

    }

    public void ActivateAbility(int weaponSlot)
    {
       // Ability abilty = Player.GetComponent<ShipStats>().GetWeaponStorage(weaponSlot);

       int x = 0;

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
        BattleUICanvas.gameObject.SetActive(inCombat);
    }

    public void EnterEncounter()
    {
        inCombat = true;
        
        BattleUICanvas.gameObject.SetActive(inCombat);
        
    }
}
