using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{

    public GameObject Player;
    
    private EncounterArea area;
    private Canvas BattleUICanvas;
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


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitEncounter()
    {
        BattleUICanvas.gameObject.SetActive(false);

    }

    void BattleScene()
    {
        
    }

    void HideBattleScene()
    {
        
    }

    public static void FleeBattleScene()
    {
        
    }

    public void EnterEncounter()
    {
        BattleUICanvas.gameObject.SetActive(true);
        
    }
}
