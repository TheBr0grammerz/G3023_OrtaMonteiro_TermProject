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
    
    

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(AbilityPressed);
        EncounterSystem.Instance.onExitCombat.AddListener(Invalidate);
    }
    
    void Invalidate()
    {
       weapon = null;
    }
    
    
     void AbilityPressed()
    {
        if (weapon != null)
        {
            Ship enemyShip = EncounterSystem.Instance._enemy?.GetComponent<Ship>();
            Ship PlayerShip = EncounterSystem.Instance.Player?.GetComponent<Ship>();
            EncounterSystem.Instance.ActivateWeapon(PlayerShip,enemyShip,weapon);
        }
    }
    // Update is called once per frame

}
