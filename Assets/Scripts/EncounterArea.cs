using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
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
        var circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = areaStats.areaRadius;
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
