using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI.MINIMAX;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Ship _ship;

    public Ship targetShip;


    private void Awake()
    {
        if (_ship == null)
        {
            _ship = GetComponent<Ship>();
        }
    }

    void DecideAttack()
    {
        if (!targetShip || _ship.weapons.Count < 1) return;
        
        //Todo: Create logic to decide which is the best weapon to use and  attack the target ship with that weapon
        EncounterState BaseState = EncounterState.Clone(EncounterSystem.Instance._currentState);
        MiniMaxTree<EncounterState> Tree = new MiniMaxTree<EncounterState>(BaseState, true, 3);
        EncounterState bestMove = Tree.GetBestMove(3,
            (state) => state.GetPossibleStates(),
            (state) => state.HeuristicEvaluation());

        if (!CheckForWeapon(_ship, bestMove.GetLastWeaponUsed()))
        {
            Debug.Log("Ship does not have the weapon in arsenal");
            return;
        }
        
        
        
        EncounterSystem.Instance.ActivateWeapon(_ship,targetShip,bestMove.GetLastWeaponUsed());
    }
    // Start is called before the first frame update
    void Start()
    {
        #region Assign Weapons
        
            //Adding First Weapon
            var weapon = Resources.Load("Weapons/OPLaser") as BaseWeapon;
            WeaponSlot slot = new WeaponSlot(weapon);
            _ship.weapons.Add(slot);
            
            //Adding Second Weapon
            weapon = Resources.Load("Weapons/DinkyLaser") as BaseWeapon;
            slot = new WeaponSlot(weapon);
            _ship.weapons.Add(slot);
            
            //Adding Third Weapon
            weapon = Resources.Load("Weapons/DinkyLaser 1") as BaseWeapon;
            slot = new WeaponSlot(weapon);
            _ship.weapons.Add(slot);
            
            //Adding Fourth weapon
            weapon = Resources.Load("Weapons/OPLaserWeak") as BaseWeapon;
            slot = new WeaponSlot(weapon);
            _ship.weapons.Add(slot);
        #endregion
        
        Debug.Log("Adding To ships Weapons In EnemyAI");

    }

    bool CheckForWeapon(Ship ship,BaseWeapon weaponToCheckAgainst)
    {
        var weapons = ship.weapons;
        foreach (var slot in weapons)
        {
            if (slot.weaponInformation == weaponToCheckAgainst) return true;

        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_ship.currentTurn && EncounterSystem.Instance.inCombat)
        {
            DecideAttack();
        }
    }
}
