using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShipUI : MonoBehaviour
{

    void Awake()
    {
        EncounterSystem.Instance.onEnterCombat.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        
    }


    void UpdateUI(Ship EnemyShip)
    {
        var name = gameObject.name;
        GetComponent<UnityEngine.UIElements.Image>().sprite = EnemyShip.shipSprite;
    }
}
