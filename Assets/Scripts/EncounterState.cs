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
            
            //BUG: There was an issue where I wasnt calling the newstate.CurrentTurnShip and defending ship so it wouldnt be accurate
            DamageValues appliedDamage = newState.currentTurnShip.Attack(newState.defendingShip,weaponSlot.weaponInformation);

            ActionLog log = new ActionLog(newState.currentTurnShip, newState.defendingShip, weaponSlot.weaponInformation,appliedDamage, TurnCount);
            newState.UpdateEncounterState(log);
            states.AddLast(newState);
        }
        return states;
    }
    
    
    public float HeuristicEvaluation(bool isMaximizingPlayer)
    {
        // Calculate basic health advantage as difference between attacking and defending hull health
        float healthAdvantage = currentTurnShip.health.hull - defendingShip.health.hull;

        // Adjust for maximizing or minimizing player
        return isMaximizingPlayer ? healthAdvantage : -healthAdvantage;
    }



    /*public float HeuristicEvaluation(bool isMaximizingShip)
    {
        float score = 0;
        
        float attackingShipsHullPercentage = currentTurnShip.health.hull / currentTurnShip.maxHealth.hull;
        float attackingShipsShieldPercentage = currentTurnShip.health.shield / currentTurnShip.maxHealth.shield;
        
        float defendingShipsHullPercentage = defendingShip.health.hull / defendingShip.maxHealth.hull;
        float defendingShipsShieldPercentage = defendingShip.health.shield / defendingShip.maxHealth.shield;

        float hullWeight;

        //If defending ships health is critically low, then we want to increase the weight to finish them off
        if (defendingShipsHullPercentage < 0.3f)
        {
            hullWeight = 2f;
        }
        else hullWeight = 1f;

        float shieldWeight = 1f;



        


        
        return score;
    }*/
}
