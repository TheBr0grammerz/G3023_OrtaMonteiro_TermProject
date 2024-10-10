
using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct DamageValues
{
    public float HullDamage;
    public float ShieldDamage;

    public DamageValues(float hullDamage, float shieldDamage)
    {
        HullDamage = hullDamage;
        ShieldDamage = shieldDamage;
    }

    public DamageValues(DamageValues other)
    {
        HullDamage = other.HullDamage;
        ShieldDamage = other.ShieldDamage;
    }

    public static DamageValues operator -(DamageValues a, DamageValues b)
    {
        return new DamageValues(
            a.HullDamage - b.HullDamage,
            a.ShieldDamage - b.ShieldDamage);
    }
    
    public static DamageValues operator +(DamageValues a, DamageValues b)
    {
        return new DamageValues(
            a.HullDamage + b.HullDamage,
            a.ShieldDamage + b.ShieldDamage);
    }
}

public abstract class BaseWeapon : ScriptableObject
{
    public int identifier = 0;
    
    public DamageValues Damage;
    public Sprite Icon;
    
    public string Name;
    public string Description;

    public bool canActivateAbility = true;

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
        DamageValues bonusDamage = caster.GetBonusDamage();
        DamageValues totalDamage = new DamageValues(Damage + bonusDamage);
        return target.TakeDamage(totalDamage);
    }
}
