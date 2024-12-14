
using UnityEngine;

[System.Serializable]
public struct FScenes
    {

    [SerializeField] public string sceneName;
   // [SerializeField] public SceneAsset unityScene;

    [SerializeField] public int sceneIndex;
    public Scenes scene;
    #if UNITY_EDITOR
    public UnityEditor.SceneAsset unityScene;
    #endif

    #if UNITY_EDITOR
    public FScenes(string sceneName, int sceneIndex, Scenes scene,UnityEditor.SceneAsset UScene) : this()
    {
        this.sceneName = sceneName;
        this.sceneIndex = sceneIndex;
        this.scene = scene;
        this.unityScene = UScene;
    }
    #else
    public FScenes(string sceneName, int sceneIndex, Scenes scene) : this()
    {
        this.sceneName = sceneName;
        this.sceneIndex = sceneIndex;
        this.scene = scene;
    }
    #endif
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