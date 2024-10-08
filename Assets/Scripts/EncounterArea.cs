using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CircleCollider2D))]
public class EncounterArea : MonoBehaviour
{
    public AreaStats areaStats;
    
    private static bool startUpCompleted = false;
    public EncounterSystem encounterSystem;
    public bool isInside = true;
    
    private CircleCollider2D circleCollider2D;
    
    public float radius;

    private void Awake()
    {
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = radius;
        encounterSystem = GameObject.Find("EncounterManager").GetComponent<EncounterSystem>();

        //Make Sure there is an encounter loaded and is not just a null area
        if (areaStats == null) areaStats = AssetDatabase.LoadAssetAtPath<AreaStats>("Assets/ScriptableObjects/NullZone.asset");
    }

    void OnValidate()
    {
        
        
        if (circleCollider2D != null)
        {
            circleCollider2D.radius = radius;
        }
        else {circleCollider2D = GetComponent<CircleCollider2D>();}
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
