using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterArea : MonoBehaviour
{
    
    [Range(0f, 1f)]
    private float encounterChance = 0.5f;
    public string areaName = "EncounterArea";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool RollEncounter()
    {
        return Random.value <= encounterChance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
