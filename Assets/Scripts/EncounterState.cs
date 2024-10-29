using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EncounterState
{
    public Ship offendingShip;
    public Ship victimShip;
    public int TurnCount;

    public List<ActionLog> logOfActions;


    public EncounterState(EncounterState other)
    {
        offendingShip = new Ship(other.offendingShip);
        victimShip = new Ship(other.victimShip);
        TurnCount = other.TurnCount;
        logOfActions = new List<ActionLog>();
        foreach (var action in other.logOfActions)
        {
            logOfActions.Add(new ActionLog(action));
        }
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
        offendingShip = new Ship(attackingShip);
        victimShip = new Ship(defendingShip);
        this.logOfActions = new List<ActionLog>();
        logOfActions.Add(new ActionLog(offendingShip.shipName,victimShip.shipName));
        TurnCount = 0;
    }

    public void UpdateEncounterState(ActionLog log)
    {
        logOfActions.Add(log);
    }



    ~EncounterState()
    {
        
    }
    
    
}
