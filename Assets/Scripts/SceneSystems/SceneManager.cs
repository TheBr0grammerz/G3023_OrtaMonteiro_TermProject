using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MySceneSystem
{
    using static SoundManager;

    public class SceneManager : MonoBehaviour
    {
    private static SceneManager _instance;

    public static SceneManager Instance
    {
        get;
        private set;
    }
    static public Dictionary<Scenes, SceneAsset> SceneToSceneAsset = new Dictionary<Scenes, SceneAsset>();
    static public Dictionary<string, SceneAsset> NameToSceneAsset = new Dictionary<string, SceneAsset>();
    static public Dictionary<Scenes, string> SceneToName = new Dictionary<Scenes, string>();
    static public Dictionary<string, Scenes> NameToScene = new Dictionary<string, Scenes>();
    [SerializeField] private List<FScenes> _scenes;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        #region Update Dictionaries
        for(int i = 0; i < _scenes.Count;i++)
        {
            if(SceneToSceneAsset.ContainsKey(_scenes[i].scene))
            {
                if(_scenes[i].scene == Scenes.None) continue;
                Debug.LogError("DUPLICATED KEY, ONLY ALLOW ONE SCENE-ENUM per SCENE  " +_scenes[i].sceneName+" " + _scenes[i].scene);
                continue;
            } 
            SceneToSceneAsset.Add(_scenes[i].scene, _scenes[i].unityScene);
            if(!NameToSceneAsset.ContainsKey(_scenes[i].sceneName)) NameToSceneAsset.Add(_scenes[i].sceneName, _scenes[i].unityScene);
            if(!SceneToName.ContainsKey(_scenes[i].scene)) SceneToName.Add(_scenes[i].scene, _scenes[i].sceneName);
            if(!NameToScene.ContainsKey(_scenes[i].sceneName)) NameToScene.Add(_scenes[i].sceneName, _scenes[i].scene);
        }
        #endregion
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += (oldScene, newScene) => OnSceneChange?.Invoke(NameToScene[newScene.name]);
    }
    void OnEnable()
    {
        OnSceneChange += SceneChange;
    }
    void OnDisable()
    {
        OnSceneChange -= SceneChange;
    }

    static public SceneAsset LoadScene(Scenes scenes)
    {
        var asset = SceneToSceneAsset[scenes];
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToName[scenes]);
        return asset;
    }
    public static SceneAsset LoadScene(string sceneName)
    {
        var asset = NameToSceneAsset[sceneName];
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        return asset;
    }

    void SceneChange(Scenes scene)
    {
        switch (scene)
        {
            case Scenes.LandingPage:
                break;
            case Scenes.MainMenu:
                break;
            case Scenes.Instructions:
                break;
            case Scenes.Settings:
                break;
            case Scenes.Gameplay01:
                PlayMusic("START",false);
                break;
            case Scenes.Gameplay02:
                break;
            case Scenes.Gameplay03:
                break;
            case Scenes.Lose:
                break;
            case Scenes.Win:
                break;
            case Scenes.Credits:
                break;
            case Scenes.Misc01:
                break;
            case Scenes.Misc02:
                break;
            case Scenes.Misc03:
                break;
            default:
            Debug.LogError("Scene not found" + SceneToName[scene]);
                break;
        }
        //LoadScenes();
        Debug.Log("Scene Changed to " + SceneToName[scene]);
    }

    static public event Action<Scenes> OnSceneChange;

    private void Start()
    {
        #region Update Scene Values
        for (int i = 0; i < _scenes.Count; i++)
        {
            int index = UnityEngine.SceneManagement.SceneManager.GetSceneByName(_scenes[i].sceneName).buildIndex;
            _scenes[i] = new FScenes(_scenes[i].sceneName,
                                    index > _scenes[i].sceneIndex?index:_scenes[i].sceneIndex,
                                    _scenes[i].scene,
                                    _scenes[i].unityScene);
        }
        #endregion
        //SaveScenes();
    }

    void OnDestroy()
    {
        //SaveScenes();
    }

    void OnValidate()
    {
        //if(_scenes == null) LoadScenes();
        string[] files = System.IO.Directory.GetFiles("Assets/Scenes","*.unity", System.IO.SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(files[i]);
            int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).buildIndex;
            SceneAsset asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(files[i]);
            if(_scenes.Count <= i) 
            {
                _scenes.Add(new FScenes(sceneName, sceneIndex,(Scenes)(-1), asset));
            }
        }
    }

    [ContextMenu("Save Scenes")]
    void SaveScenes()
    {
        using (System.IO.StreamWriter outFile = new System.IO.StreamWriter("Assets/Scenes/Scenes.json"))
        {
            outFile.WriteLine(JsonUtility.ToJson(new SceneListWrapper { scenes = _scenes }, true));
        }
    }

    [ContextMenu("Load Scenes")]
    private void LoadScenes()
    {
        SceneListWrapper wrapper = JsonUtility.FromJson<SceneListWrapper>(System.IO.File.ReadAllText("Assets/Scenes/Scenes.json"));
        _scenes = wrapper.scenes;
    }

    [Serializable]
    private class SceneListWrapper
    {
        public List<FScenes> scenes;
    }
    }
}