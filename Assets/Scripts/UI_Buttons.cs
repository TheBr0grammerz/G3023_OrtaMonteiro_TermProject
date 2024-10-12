using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

//using UnityEngine.UIElements;

public class UI_Buttons : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    public Ship playerShip;

    

    void UpdateUIButtons(Ship enemyShip)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            BaseWeapon weapon;
            /*This statement is checking whether the index i is within the bounds of the playerShip.weapons list:
            If it is, it assigns weaponInformation of the weapon at index i to weapon.
            If it's not, it assigns null to weapon to avoid an IndexOutOfRangeException.
            */
            weapon = i <= playerShip.weapons.Count - 1 ? playerShip.weapons[i].weaponInformation : null;

            var myButton = buttons[i].GetComponent<MyButton>();
            var tempText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            
            if (weapon == null)
            {
                buttons[i].image.color = Color.red;
                tempText.text = "No Weapon Available";
                //BUG: This doesnt set the Button to not interactable when its null. Something to do with the update function of MyButton
                
                buttons[i].interactable = false;
                continue;
                
            }
            if(weapon.identifier == 0)
            {
                buttons[i].image.color = Color.yellow;
                tempText.text = "Not Yet Implemented";
                continue;
            }

            //Todo: Set MyButton data to represent a weapon slot
            tempText.text = weapon.name;
            buttons[i].image.color = Color.white;
            buttons[i].interactable = myButton.isPassive = weapon.isPassiveWeapon;
            myButton.weapon = weapon;
        }
    }

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        playerShip = EncounterSystem.Instance.Player.GetComponent<Ship>();

        EncounterSystem.Instance.onEnterCombat.AddListener(UpdateUIButtons);
    }

    void Start()
    {
        
    }

    private void OnDestroy()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
