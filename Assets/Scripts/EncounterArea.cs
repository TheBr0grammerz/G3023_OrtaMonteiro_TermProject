using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EncounterArea : MonoBehaviour
{
    public AreaStats areaStats;

    [SerializeField]
    private static bool startUpCompleted = false;
    public EncounterSystem encounterSystem;
    public bool isInside = true;

    private void Awake()
    {
        GetComponent<CapsuleCollider2D>().size = new Vector2(areaStats.areaRadius, 0.5f);
        encounterSystem = GameObject.Find("EncounterManager").GetComponent<EncounterSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartUpCompleted), 1);
    }

    void StartUpCompleted()
    {
        startUpCompleted = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (startUpCompleted)
        {
            if (other.CompareTag("Player"))
            {
                isInside = true;
                encounterSystem.EnteredArea(this);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (startUpCompleted)
        {
            if(other.CompareTag("Player"))
            {
                isInside = false;
                encounterSystem.ExitedArea(this);
            }
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
