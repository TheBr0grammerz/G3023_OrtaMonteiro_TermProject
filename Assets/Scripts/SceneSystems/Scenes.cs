
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct FScenes
    {

    [SerializeField] public string sceneName;
   // [SerializeField] public SceneAsset unityScene;

    [SerializeField] public int sceneIndex;
    public Scenes scene;
    public SceneAsset unityScene;


    public FScenes(string sceneName, int sceneIndex, Scenes scene,SceneAsset UScene) : this()
    {
        this.sceneName = sceneName;
        this.sceneIndex = sceneIndex;
        this.scene = scene;
       this.unityScene = UScene;
    }
}

[System.Serializable]
public enum Scenes
{
    None = -1,
    LandingPage,
    MainMenu,
    Instructions,
    Settings,
    Gameplay01,
    Gameplay02,
    Gameplay03,
    Lose,
    Win,
    Credits,
    Misc01,
    Misc02,
    Misc03,
}