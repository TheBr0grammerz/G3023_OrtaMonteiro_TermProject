using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : UnityEngine.ScriptableObject
{
    public DamageValues damageValues;
    public float duration;

    public abstract void ApplyEffect(Ship target);


}
