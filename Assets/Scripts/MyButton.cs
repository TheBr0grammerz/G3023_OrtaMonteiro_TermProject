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
    private static Ship playerShip;
    
    

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(AbilityPressed);
        EncounterSystem.Instance.onExitCombat.AddListener(Invalidate);
        EncounterSystem.Instance.onEnterCombat.AddListener((eShip) =>
        {
            enemyShip = eShip;
            playerShip = EncounterSystem.Instance.Player;
        });
        enemyShip = EncounterSystem.Instance.Enemy;
        playerShip = EncounterSystem.Instance.Player;
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
            GetComponent<UnityEngine.UI.Button>().interactable = playerShip.currentTurn;
        }
    }


    void AbilityPressed()
    {
        if (weapon == null || enemyShip == null) return;
        
        
        //Todo: Find a way to get latest actionLog and display that to text animation
        GetComponentInParent<Animator>().SetTrigger("PlayerAttack");
        EncounterSystem.Instance.ActivateWeapon(playerShip,enemyShip,weapon);
    }
}
