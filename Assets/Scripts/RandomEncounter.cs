using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class RandomEncounter : MonoBehaviour
{
    private Rigidbody2D rb;
    public float distanceTravelledSinceLastEncounter;

    public float distanceTraveled = 0;
    

    
    [FormerlySerializedAs("_encounterManager")] [SerializeField]
    private EncounterSystem encounterSystem;
    
    
    [Range(0f,10000f)]
    [SerializeField]
    private float minEncounterDistance = 2f;
    
    [SerializeField]
    EncounterArea CurrentEncounterArea;
    // Start is called before the first frame update
    void Start()
    {
        encounterSystem = GameObject.Find("EncounterManager").GetComponent<EncounterSystem>();
        rb = encounterSystem.Player.GetComponent<Rigidbody2D>();
        
        // SceneManager.LoadScene("Battle");
    }

    private void Update()
    {
        if (rb.velocity.magnitude >0)
        {
            distanceTravelledSinceLastEncounter += rb.velocity.magnitude * Time.deltaTime;
            distanceTraveled += rb.velocity.magnitude * Time.deltaTime;
            if (distanceTravelledSinceLastEncounter >= minEncounterDistance)
            {
                distanceTravelledSinceLastEncounter = 0;
                
                
                //if (CurrentEncounterArea.RollEncounter() || true)
                if ( true)
                {
                    //This is where the encounter begins
                    encounterSystem.EnterEncounter();
                    
                    Debug.Log("Encounter entered");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
         CurrentEncounterArea = other.gameObject.GetComponent<EncounterArea>();

    }
}
