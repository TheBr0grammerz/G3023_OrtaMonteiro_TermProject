using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField]
    int health = 100;

    [SerializeField] private WeaponSlot[] _weaponSlots;


    public void TakeDamage(int damage)
    {
        health -= damage;
    }


    public WeaponInformation GetEffectAtWeaponSlot(int slot)
    {
        if (_weaponSlots[slot - 1] == null) return null;
        
        
        return _weaponSlots[slot-1].weaponInformation;
    }
    
    //#########################################################//

    void TakeDamage()
    {
        health -= 1;
    }
    void Start()
    {
       // GetComponent<EncounterSystem>().OnAttack += TakeDamage();
    }
}
