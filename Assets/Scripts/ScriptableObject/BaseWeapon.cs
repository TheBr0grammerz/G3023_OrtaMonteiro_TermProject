
using System;
using UnityEngine;
using UnityEngine.Serialization;




public abstract class BaseWeapon : ScriptableObject
{
    public int identifier = 0;
    
    public DamageValues Damage;
    public Sprite Icon;
    
    public string Description;

    public bool isPassiveWeapon = false;

    public StatusEffect effectToApply;



    /// <summary>
    /// Reduces the target ship's health based on the damage values provided.
    /// Applies the specified hull and shield damage, ensuring values cannot go below zero.
    /// </summary>
    /// <returns>
    /// A <see cref="DamageValues"/> representing the actual damage applied
    /// to the target.
    /// </returns>
    public virtual DamageValues ApplyDamage(Ship caster, Ship target)
    {

        effectToApply.ApplyEffect(target);
        DamageValues bonusDamage = caster.GetBonusDamage();
        DamageValues totalDamage = Damage + bonusDamage;
        return target.TakeDamage(totalDamage);
    }
}
