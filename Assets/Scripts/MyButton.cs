using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{

    public BaseWeapon weapon;
    private static Ship enemyShip;
    public bool isPassive = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(AbilityPressed);
        EncounterSystem.Instance.onExitCombat.AddListener(Invalidate);
        
        enemyShip = EncounterSystem.Instance.Enemy;


    }
    
    void Invalidate()
    {
       weapon = null;
       enemyShip = null;
    }

    private void Update()
    {
        if (!isPassive)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = EncounterSystem.Instance.Player.currentTurn;
        }
    }


    void AbilityPressed()
    {
        if (weapon != null && enemyShip != null)
        {
            
            EncounterSystem.Instance.Player?.Attack(enemyShip,weapon);
        }
    }
    // Update is called once per frame

}
