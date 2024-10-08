using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

//using UnityEngine.UIElements;

public class UI_Buttons : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    private EncounterSystem _encounterSystem;


    private void OnDestroy()
    {
        //EncounterSystem.Instance.onEnterCombat -= UpdateUIButtons;
    }

    void UpdateUIButtons()
    {
        buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            var myButt = button.GetComponent<MyButton>();
            
            //Todo: Set the color or IMG to match what weapon type it is
            
            
            //Todo: Set MyButton data to represent a weapon slot
            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateUIButtons();
        EncounterSystem.Instance.onEnterCombat.AddListener(UpdateUIButtons);
    }
    
}
