using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    
    private TextMeshProUGUI textMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
