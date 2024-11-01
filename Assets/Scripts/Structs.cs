using System;



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

    public override string ToString()
    {
        return $"HullDamage: {HullDamage}, ShieldDamage: {ShieldDamage}";
    }
}