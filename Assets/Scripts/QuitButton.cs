using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void RequestFlee()
    {
        EncounterSystem.Instance.FleeBattleScene();
    }

    

    }

