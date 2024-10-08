using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public int slotIdentifier = 0;
    public Effect effect;
    
    public bool useableAbility = false;

    void Init(bool useableAbility, Effect effect, int slotIdentifier)
    {
        this.useableAbility = useableAbility;
        this.effect = effect;
        this.slotIdentifier = slotIdentifier;
    }


    void Awake()
    {
        
    }
    public void AbilityPressed()
    {
        EncounterSystem.Instance.ActivateAbility(slotIdentifier);
    }
}
