using UnityEngine;

[System.Serializable]
public class WeaponSlot 
{
    //public Texture2D Texture { get;private set; }
    public int WeaponIdentifier { get;private set; }
    public BaseWeapon weaponInformation;
    
    public bool canUseAbility = false;

    public WeaponSlot(BaseWeapon weapon)
    {
        weaponInformation = weapon;
    }

}
