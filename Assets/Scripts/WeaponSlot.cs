using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class WeaponSlot
{

    public Texture2D Texture { get;private set; }
    public int WeaponIdentifier { get;private set; }
    public BaseWeapon weaponInformation;
    
    public bool canUseAbility = false;

    public WeaponSlot(BaseWeapon weapon)
    {
        weaponInformation = weapon;
        canUseAbility = weapon.canActivateAbility;
    }
}
