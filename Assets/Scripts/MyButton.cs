using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    
    [HideInInspector]
    public int buttonIdentifier = -1;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Button>().onClick.AddListener(OnRequestAbility(buttonNumber));
        
    }

    void OnAbilityPressed()
    {
        EncounterSystem.Instance.ActivateAbility(buttonIdentifier);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
