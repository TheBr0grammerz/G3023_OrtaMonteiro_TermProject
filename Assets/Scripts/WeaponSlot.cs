using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSlot : MonoBehaviour
{
    
    public Texture2D texture { get;private set; }
    public int WeaponIdentifier { get;private set; }
    public WeaponInformation weaponInformation;
    
    public bool canUseAbility = false;
    void Init(bool useableAbility, WeaponInformation weaponInformation, int weaponIdentifier)
    {
        this.canUseAbility = useableAbility;
        this.weaponInformation = weaponInformation;
        this.WeaponIdentifier = weaponIdentifier;
    }


    void Awake()
    {
        
    }
    public void AbilityPressed()
    {
        EncounterSystem.Instance.ActivateAbility(WeaponIdentifier);
    }
}
