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
    private static Ship _enemyShip;
    public bool isPassive = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(AbilityPressed);
        EncounterSystem.Instance.onEnterCombat.AddListener((enemyShip) => { _enemyShip = enemyShip; }); 
        EncounterSystem.Instance.onExitCombat.AddListener(Invalidate);
        
    }


    
    void Invalidate()
    {
       weapon = null;
       _enemyShip = null;
    }

    private void Update()
    {
        if (!isPassive)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = EncounterSystem.Instance.Player.currentTurn;
        }
    }


    public void AbilityPressed()
    {
        if (weapon != null && _enemyShip != null)
        {
            EncounterSystem.Instance.Player?.Attack(_enemyShip,weapon);
            //Todo: Find a way to get latest actionLog and display that to text animation
            GetComponentInParent<Animator>().SetTrigger("PlayerAttack");    
        }
    }
   

}
