using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

//using UnityEngine.UIElements;

public class UI_Buttons : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        int slotNum = 1;
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Weapon Slot " + slotNum;
            slotNum++;
        }
    }

    bool UpdateUI()
    {

        
        
        return true;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
