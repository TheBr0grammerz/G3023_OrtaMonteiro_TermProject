using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophySystem : MonoBehaviour
{

    [SerializeField] private List<TrophySO> earnableTrophies = new List<TrophySO>();
    public Dictionary<string, Trophy> trophies = new Dictionary<string, Trophy>();

    [SerializeField] private TrophyUIController TrophyUIController;

    private void Awake()
    {
        foreach (var trophyScritableObject in earnableTrophies)
        {
            if(trophies.ContainsKey(trophyScritableObject.trophyName))
                continue;
            
            trophies.Add(trophyScritableObject.trophyName, trophyScritableObject.ToTrophy());
        }
    }


    public bool IsUnlocked(string name)
    {
        if (trophies.ContainsKey(name))
        {
            if(trophies[name].isUnlocked) return true;
            return false;
        }

        return false;
        
    }

    public void UpdateScore(string name, int score)
    {
        if(!IsUnlocked(name))
        {
            trophies[name].currentScore += score;
            Debug.Log("Trophy Score Updated: " + name + " Current Score: " + trophies[name].currentScore);
            if (trophies[name].currentScore >= trophies[name].requiredScore)
            {
                UnlockTrophy(name);
            }
        }

        
    }

    public void UnlockTrophy(string name)
    {
        if (IsUnlocked(name)) return;
        
        trophies[name].isUnlocked = true;
        StartCoroutine(TrophyUIController?.PlayAnimation(trophies[name]));
        //TrophyUIController?.PlayAnimation(trophies[name]);
    }
}
    


