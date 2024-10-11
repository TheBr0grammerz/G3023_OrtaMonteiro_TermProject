using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class EnemyShipUI : MonoBehaviour
{
    
    public Image Image;
    public TextMeshProUGUI ShipName;

    public bool isPlayer = false;

    void Awake()
    {
        EncounterSystem.Instance.onEnterCombat.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        
    }


    void UpdateUI(Ship EnemyShip)
    {
        if (!isPlayer)
        {
            Image.sprite = EnemyShip.shipSprite;
            ShipName.text = EnemyShip.name;
        }
        else
        {
            Ship playerShip = EncounterSystem.Instance.Player.GetComponent<Ship>();
            Image.sprite = playerShip.shipSprite;
            ShipName.text = playerShip.name;
        }

    }
}
