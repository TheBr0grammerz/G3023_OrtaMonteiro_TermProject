using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    // Start is called before the first frame update
    EncounterSystem system = null;
    void Start()
    {
        foreach (var thing in SceneManager.GetSceneByName("SpaceScene").GetRootGameObjects())
        {
            if (thing.CompareTag("EncounterManager"))
            {
                system = thing.GetComponent<EncounterSystem>();
            }
        }
            
        GetComponent<Button>().onClick.AddListener(RequestFlee);
    }

    void RequestFlee()
    {
        system.FleeBattleScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
