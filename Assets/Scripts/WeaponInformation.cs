using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty Effect", menuName = "Scriptable Object/New Weapon")]
public abstract class WeaponInformation : ScriptableObject
{
    public abstract void ApplyEffect();
}
