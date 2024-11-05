using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Ship _ship;

    public Ship targetShip;


    private void Awake()
    {
        if (_ship == null)
        {
            _ship = GetComponent<Ship>();
        }
    }

    void DecideAttack()
    {
        if (!targetShip || _ship.weapons.Count < 1) return;
        
        //Todo: Create logic to decide which is the best weapon to use and  attack the target ship with that weapon
        
        
        BaseWeapon weaponUsed = _ship.SelectWeaponAt(0);
        _ship.Attack(targetShip, weaponUsed);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_ship.currentTurn && EncounterSystem.Instance.inCombat)
        {
            DecideAttack();
        }
    }
}
