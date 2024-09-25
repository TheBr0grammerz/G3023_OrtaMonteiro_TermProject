using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomEncounter : MonoBehaviour
{
    private Rigidbody rb;
    public float distanceTravelledSinceLastEncounter;

    public float distanceTraveled = 0;
    
    [SerializeField]
    private EncounterManager _encounterManager;
    
    [Range(0f,10000f)]
    [SerializeField]
    private float minEncounterDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _encounterManager = GameObject.Find("EncounterManager").GetComponent<EncounterManager>();
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.IsUnityNull()) return;
        
        EncounterArea area = other.gameObject.GetComponent<EncounterArea>();
        if (area is not null)
        {
            if (rb.velocity.magnitude >0)
            {
                distanceTravelledSinceLastEncounter += rb.velocity.magnitude * Time.deltaTime;
                distanceTraveled += rb.velocity.magnitude * Time.deltaTime;
                if (distanceTraveled >= minEncounterDistance)
                {
                    distanceTravelledSinceLastEncounter = 0;
                    if (area.RollEncounter())
                    {
                        //This is where the encounter begins
                        _encounterManager.EnterEncounter(area);
                        Debug.Log(area.areaName);
                    }
                }
            }
        }
    }
}
