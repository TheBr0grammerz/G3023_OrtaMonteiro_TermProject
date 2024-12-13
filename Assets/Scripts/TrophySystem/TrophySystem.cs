using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrophySystem : MonoBehaviour
{

   
    public Dictionary<string, Trophy> trophies = new Dictionary<string, Trophy>();

    [SerializeField] private TrophyUIController TrophyUIController;

    private void Awake()
    {
        var Trophies = Resources.LoadAll<TrophySO>("Trophies");
        
        foreach (var trophyScritableObject in Trophies)
        {
            if(trophies.ContainsKey(trophyScritableObject.trophyName))
                continue;
            
            trophies.Add(trophyScritableObject.trophyName, trophyScritableObject.ToTrophy());
        }
    }

    void Start()
    {
        
    }
    void OnDisable()
    {
        if(TrophyUIController != null) TrophyUIController.gameObject.SetActive(false);
    }

    public GameObject FindTrophyUIController()
    {
        var canvasObjects = UnityEngine.SceneManagement.SceneManager.GetSceneByName("SpaceScene").GetRootGameObjects();
        if(canvasObjects == null)
        {
            Debug.LogError("Canvas Objects not found"); 
            return null;
        }
        foreach(var obj in canvasObjects)
        {
            if(obj.GetComponent<TrophyUIController>())
            {
                TrophyUIController = obj.GetComponent<TrophyUIController>();
                break;
            }
        }
        TrophyUIController.gameObject.SetActive(true);
        

        return TrophyUIController.gameObject;
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
    


