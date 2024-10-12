using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstTimePlaying : MonoBehaviour
{
    private Ship ship;

    public static bool firstTimePlaying = true;
    
    public BaseWeapon[] weaponData;


    [ContextMenu("Add Weapons")]
    void AddWeapons()
    {
        GameObject player = GameObject.Find("Player");
        ship = player.GetComponent<Ship>();
            
        var firstWeapon = new WeaponSlot(weaponData[0]);
        var secondWeapon = new WeaponSlot(weaponData[1]);
            
        ship.weapons.Add(firstWeapon);
        ship.weapons.Add(secondWeapon);
    }
    // Start is called before the first frame update
    void Start()
    {
        AddWeapons();
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
