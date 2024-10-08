using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterZone", menuName = "Scriptable Object/New Encounter Zone")]
public class AreaStats : ScriptableObject
{
    public string encounterZoneName;

    [Range(0f, 1f)] public float encounterZoneChance;
    

    public float areaRadius;

}
