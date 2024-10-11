using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarAdjuster : MonoBehaviour
{
    public Slider HullSlider;
    public Slider ShieldSlider;
    private Ship _ship;
    public bool isPlayer = false;
    
    
    private void Awake()
    {
        EncounterSystem.Instance.onEnterCombat.AddListener(LinkShipToUI);
        EncounterSystem.Instance.onExitCombat.AddListener(ExitedCombat);
    }
    

    private void LinkShipToUI(Ship enemyShip)
    {
        _ship = isPlayer ? EncounterSystem.Instance.Player.GetComponent<Ship>() : enemyShip;
    }

    void ExitedCombat()
    {
        _ship = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        HullSlider.maxValue = _ship.maxHealth.hull;
        ShieldSlider.maxValue = _ship.maxHealth.shield;
    }

    // Update is called once per frame
    void Update()
    {
        HullSlider.value = _ship.health.hull;
        ShieldSlider.value = _ship.health.shield;
    }
}
