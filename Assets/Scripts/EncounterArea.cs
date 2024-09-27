using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EncounterArea : MonoBehaviour
{
    public AreaStats areaStats;

    public EncounterSystem encounterSystem;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider2D>().size = new Vector2(areaStats.areaRadius, 0.5f);
        encounterSystem = GameObject.Find("EncounterManager").GetComponent<EncounterSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            encounterSystem.zones.Enqueue(this);
            Debug.Log(areaStats.name);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            encounterSystem.zones.Dequeue();
        }
    }



    public bool RollEncounter()
    {
        return Random.value <= areaStats.encounterZoneChance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
