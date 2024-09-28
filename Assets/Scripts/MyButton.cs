using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    
    [HideInInspector]
    public int buttonNumber = -1;
    private TextMeshProUGUI textMesh;

    public EncounterSystem _encounterSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        //GetComponent<Button>().onClick.AddListener(OnRequestAbility(buttonNumber));
        
    }

    private UnityAction OnRequestAbility(int i)
    {


        //_encounterSystem.ActivateAbility(i);
        return null;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
