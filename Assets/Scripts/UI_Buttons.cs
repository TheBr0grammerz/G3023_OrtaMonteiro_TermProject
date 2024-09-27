using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // Start is called before the first frame update
    void Start()
    {
        //Finds a reference to the EncounterSystem
        GameObject[] objects = SceneManager.GetSceneByName("SpaceScene").GetRootGameObjects();
        foreach (GameObject obj in objects)
        {
            if (obj.CompareTag("EncounterManager"))
            {
                _encounterSystem = obj.GetComponent<EncounterSystem>();
            }
        }
        
        
        //Finds All buttons in the Attached UI and Initializes its data
        buttons = GetComponentsInChildren<Button>();
        int slotNum = 1;
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Weapon Slot " + slotNum;
            var myButt = button.GetComponent<MyButton>();
            myButt.buttonNumber = slotNum;
            myButt._encounterSystem = _encounterSystem;
            //button.onClick.AddListener(UpdateUI(slotNum));
            slotNum++;
           // UnityAction<>
        }
    }
    
}
