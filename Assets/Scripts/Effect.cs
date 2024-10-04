using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty Effect", menuName = "Scriptable Object/New Effect")]
public abstract class Effect : ScriptableObject
{
    public abstract void ApplyEffect();
}
