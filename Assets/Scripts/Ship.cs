using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct HealthPools
{
    public float hull;
    public float shield;

    public HealthPools(float hull, float shield)
    {
        this.hull = hull;
        this.shield = shield;
    }

    public HealthPools(HealthPools maxHealth)
    {
        this.hull = maxHealth.hull;
        this.shield = maxHealth.shield;
    }

    public static HealthPools operator +(HealthPools a,DamageValues b)
    {
        return new HealthPools(
            a.hull + b.HullDamage,
            a.shield + b.ShieldDamage);
    }
    
    public static HealthPools operator -(HealthPools a,DamageValues b)
    {
        return new HealthPools(
            a.hull - b.HullDamage,
            a.shield - b.ShieldDamage);
    }

    public static HealthPools operator *(HealthPools a, float b)
    {
        return new HealthPools(
            a.hull * b,
            a.shield * b);
    }

    public override string ToString()
    {
        return $"Hull Health: {hull}\nShield Health: {shield}";
    }
    
}
public class Ship : MonoBehaviour
{
    [SerializeField]
    public List<WeaponSlot> weapons;
    
    
    public Sprite shipSprite;

    public HealthPools health;

    public HealthPools maxHealth = new HealthPools(100, 100);
    public bool currentTurn;

    public string shipName = "";

    private void OnDeath()
    {
       //EncounterSystem.Instance.FleeBattleScene();
     
       //Destroy(gameObject);
    }

    public Ship(Ship other)
    {
        weapons = other.weapons;
        shipSprite = other.shipSprite;
        health = other.health;
        maxHealth = other.maxHealth;
        currentTurn = other.currentTurn;
        shipName = other.shipName;
    }

    private void OnValidate()
    {
        
    }

    private void Awake()
    {
        weapons = new List<WeaponSlot>();
        GetComponent<SpriteRenderer>().sprite = shipSprite;
        if (health is { hull: <= 0, shield: <= 0 }) health = new HealthPools(maxHealth);
        if (shipName == "") shipName = gameObject.name;
    }

    public BaseWeapon SelectWeaponAt(int index)
    {
        return weapons[index].weaponInformation;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DamageValues GetBonusDamage()
    {
        //Todo: Create a method that calculates bonus damage depending on amount of weapons currently in weaponslot

        float hullBonusDmg = 0;
        float shieldBonusDmg = 0;
        return new DamageValues(hullBonusDmg, shieldBonusDmg);
    }

    public DamageValues Attack( Ship targetShip,BaseWeapon WeaponUsed)
    {
        DamageValues appliedDamage = WeaponUsed.ApplyDamage(this, targetShip);
        return appliedDamage;
    }
    
    
/// <summary>
/// 
/// </summary>
/// <param name="damageValues"> used to calculate the amount of damage to receive</param>
/// <returns> a <see cref="DamageValues"/> representing the actual damage applied</returns>
    public DamageValues TakeDamage(DamageValues damageValues)
    {
        HealthPools startingHealth = new HealthPools(health.hull, health.shield);

        health -=  damageValues;
        health.hull = Mathf.Clamp(health.hull, 0, maxHealth.hull);
        health.shield = Mathf.Clamp(health.shield, 0, maxHealth.shield);
        if (health.hull <= 0) OnDeath();
        if (health.shield <= 0) OnShieldDown();

        //health.hull -= damageValues.HullDamage;
        //health.shield -= damageValues.ShieldDamage;
        
        float appliedHullDamage = startingHealth.hull - Mathf.Max(health.hull, 0);
        float appliedShieldDamage = startingHealth.shield - Mathf.Max(health.shield, 0);

        return new DamageValues(appliedHullDamage, appliedShieldDamage);
    }

    private void OnShieldDown()
    {
    }
}
