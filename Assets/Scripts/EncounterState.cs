using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class EncounterState
{
    public Ship currentTurnShip;
    public Ship defendingShip;
    public int TurnCount;
    public bool isGameOver = false;

    public List<ActionLog> logOfActions;


    public EncounterState(EncounterState other)
    {
        currentTurnShip = new Ship(other.currentTurnShip);
        defendingShip = new Ship(other.defendingShip);
        TurnCount = other.TurnCount;
        logOfActions = new List<ActionLog>();
        foreach (var action in other.logOfActions)
        {
            logOfActions.Add(new ActionLog(action));
        }
    }

    public static EncounterState Clone(EncounterState other)
    {
        return new EncounterState(other);
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="player">Player Ship</param>
    /// <param name="enemy"> Enemy Ship</param>
    /// <param name="firstTurn">Ship that started combat</param>
    /// <param name="secondTurn">Ship that was initiated on</param>
    public EncounterState(Ship attackingShip,Ship defendingShip)
    {
        currentTurnShip = attackingShip;
        this.defendingShip = defendingShip;
        this.logOfActions = new List<ActionLog>();
        logOfActions.Add(new ActionLog(currentTurnShip.shipName,this.defendingShip.shipName));
        TurnCount = 0;
    }

    private EncounterState()
    {
    }


    public void UpdateEncounterState(ActionLog log)
    {
        (currentTurnShip.currentTurn,defendingShip.currentTurn) = (defendingShip.currentTurn,currentTurnShip.currentTurn);
        (currentTurnShip,defendingShip) = (defendingShip,currentTurnShip);
        logOfActions.Add(log);
        TurnCount++;
    }

    ~EncounterState()
    {
        
    }

    public bool IsTerminalState()
    {
        return (currentTurnShip.health.hull <= 0 || defendingShip.health.hull <= 0);
    }

    public BaseWeapon GetLastWeaponUsed()
    {
        BaseWeapon weaponUsed = logOfActions.Last().weapon;
        return weaponUsed;
    }


    public LinkedList<EncounterState> GetPossibleStates()
    {
        LinkedList<EncounterState> states = new LinkedList<EncounterState>();
        foreach (var weaponSlot in currentTurnShip.weapons)
        {
            EncounterState newState = new EncounterState(this);
            
            DamageValues appliedDamage = newState.currentTurnShip.Attack(defendingShip,weaponSlot.weaponInformation);

            ActionLog log = new ActionLog(currentTurnShip, defendingShip, weaponSlot.weaponInformation,appliedDamage, TurnCount);
            newState.UpdateEncounterState(log);
            states.AddLast(newState);
        }
        return states;
    }


    public int HeuristicEvaluation()
    {
        int score = 0;
        float shieldWeight = 0.3f;
        float hullWeight = 0.7f;

        float shieldEvaluation = currentTurnShip.health.shield * shieldWeight;
        float hullEvaluation = currentTurnShip.health.hull * hullWeight;
        

        score += Mathf.RoundToInt(shieldEvaluation);
        score += Mathf.RoundToInt(hullEvaluation);
        
        return score;
    }
}
