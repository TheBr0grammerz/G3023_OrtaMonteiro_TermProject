using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct ActionLog
{
    public string CasterName;
    public string TargetName;
    public BaseWeapon weapon;
    public string Description;
    public DamageValues DamageApplied;
    public int turn;

    /// <summary>
    /// Constructor called when the very first encounter happens
    /// </summary>
    /// <param name="FirstCharacterName">Ship that started combat</param>
    /// <param name="secondCharacterName">Second Ship in order</param>
    public ActionLog(string FirstCharacterName, string secondCharacterName)
    {
        CasterName = FirstCharacterName;
        TargetName = secondCharacterName;
        turn = 0;
        weapon = null;
        DamageApplied = default;
        Description = $"{CasterName} began combat with {TargetName}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionLog"/> class with details of an attack action.
    /// </summary>
    /// <param name="Caster">The ship that performed the attack.</param>
    /// <param name="Target">The ship that was targeted by the attack.</param>
    /// <param name="weaponUsed">The weapon used by the caster in the attack.</param>
    /// <param name="DamageDelt">The damage dealt to the target as a result of the attack.</param>
    public ActionLog(Ship Caster, Ship Target, BaseWeapon weaponUsed,DamageValues DamageDelt,int turn)
    {
        this.DamageApplied = DamageDelt;
        this.turn = turn;
        CasterName = Caster.shipName;
        TargetName = Target.shipName;
        weapon = weaponUsed;
        Description = $"{Caster.shipName} uses a {weaponUsed.name} to attack {Target} dealing a total of {this.DamageApplied.ToString()} ";
    }

    public ActionLog(ActionLog other)
    {
        DamageApplied = other.DamageApplied;
        turn = other.turn;
        CasterName = other.CasterName;
        TargetName = other.TargetName;
        weapon = other.weapon;
        Description = other.Description;
    }

    public override string ToString()
    {
       // return $"DamageApplied: {DamageApplied} \n" +
       //        $"Turn: {turn} \n" +
       //        $"CasterName: {CasterName} \n" +
       //        $"TargetName: {TargetName} \n" +
       //        $"Weapon Used: {weapon.name} \n" +
       //        $"Description: {Description}";
       
       return Description;
    }
}
