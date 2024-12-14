using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    /*****************************************************/
    /******************* Music ***************************/
    /*****************************************************/
    #region Music Variables
    public Dictionary<string, AudioClip> Music { get;private set; }
    [SerializeField] private List<string> _musicKeyNames = new List<string>();

    private float _musicVolume;

    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            float clmpValue = Mathf.Clamp(value / 100f, 0f, 1f);
            foreach (var key in Music.Keys)
            {
                if (IsPlaying(key, out var audioSource))
                {
                    audioSource.volume = clmpValue;
                }
            }
        }
    }
    #endregion
    /*****************************************************/
    /******************* SFX******************************/
    /*****************************************************/
    #region SFX Variables
    public Dictionary<string, AudioClip> SFX { get;private set; }

    [SerializeField] private List<string> _SFXKeyNames = new List<string>();

    private float _SFXVolume;

    public float SFXVolume
    {
        get { return _SFXVolume; }
        set
        {
            _SFXVolume = value;
            float normalizedValue = value / 100f;
            float clmpValue = Mathf.Clamp(normalizedValue, 0f, 1f);

            foreach (var key in SFX.Keys)
            {
                if (IsPlaying(key, out var audioSource))
                {
                    audioSource.volume = clmpValue;
                }
            }
        }
    }
    #endregion
    
    
    
    /*****************************************************/
    /********************Debugging **********************/
    /*****************************************************/
    [Header("Debugging")]
    private GameObject parentForSources;
    [SerializeField] private int _sourceCount = 0;

    private LinkedList<AudioSource> _sources;
    public static SoundManager Instance { get; private set; }

    

    [Range(0,100)]public float EditorMusicVolume;
    [Range(0,100)]public  float EditorSFXVolume;
    private static string currentMusic = "";

    private void Awake()
    {
        
        #region Instance Creation
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        #endregion
        
        #region Initialization of Variables
        Music = new Dictionary<string, AudioClip>();
        SFX = new Dictionary<string, AudioClip>();
        _sources = new LinkedList<AudioSource>();
        parentForSources = new GameObject("AudioSources");
        parentForSources.transform.SetParent(this.transform);
        #endregion
        

        
        #region Populating Sounds
        var clips =  Resources.LoadAll<AudioClip>("Sounds/Music");
        foreach (var clip in clips)
        {
            _sources.AddLast(parentForSources.AddComponent<AudioSource>());
            _sourceCount++;   
            Music.Add(clip.name, clip);
        }

        clips = Resources.LoadAll<AudioClip>("Sounds/SFX");
        foreach (var clip in clips)
        {
            _sources.AddLast(parentForSources.AddComponent<AudioSource>());
            _sourceCount++;
            SFX.Add(clip.name, clip);
        }
        #endregion
    }
    private static AudioSource Play(string keyname, Dictionary<string, AudioClip> dictionary)
    {

        if (!dictionary.TryGetValue(keyname, out var sound))
        {
            Debug.LogError("Sound not found in Music or SFX Dictionary. Key: " + keyname);
            return null;
        }
        if (Instance._sources.First.Value.isPlaying)
        {
            if(Instance._sources.Last.Value.isPlaying)
            {
                Instance._sources.AddFirst(Instance.parentForSources.AddComponent<AudioSource>());
                Instance._sourceCount++;
            }
            
            #region Move to the end of the list
            LinkedListNode<AudioSource> firstNode = Instance._sources.First;
            Instance._sources.RemoveFirst();
            Instance._sources.AddLast(firstNode);
            #endregion
            return Play(keyname, dictionary);
        }

        Instance._sources.First.Value.clip = sound;
        Instance._sources.First.Value.Play();
        if(Instance._SFXKeyNames.Contains(keyname))
            Instance._sources.First.Value.volume = Instance.SFXVolume;
        else if(Instance._musicKeyNames.Contains(keyname))
            Instance._sources.First.Value.volume = Instance.MusicVolume;

        #region Move to the end of the list
        LinkedListNode<AudioSource> fn = Instance._sources.First;
        Instance._sources.RemoveFirst();
        Instance._sources.AddLast(fn);
        #endregion
        return Instance._sources.Last.Value;
    }
    

    static private bool IsPlaying(string keyname,out AudioSource audioSource)
    {
        bool foundInMusic = Instance.Music.TryGetValue(keyname, out var musicClip);
        bool foundInSFX = Instance.SFX.TryGetValue(keyname, out var SFXClip);

        if (!foundInMusic && !foundInSFX)
        {
            audioSource = null;
            return false;
        }


        foreach (var source in Instance._sources ?? Enumerable.Empty<AudioSource>())
        {
            if (source.clip == null)
            {
                continue;
            }

            if(source.clip == musicClip || source.clip == SFXClip)
            {
                audioSource = source;
                return true;
            }
        }
        audioSource = null;
        return false;
    }

    public static void PlayMusic(string keyname,bool bInterrupt)
    {
        /* Check if the music is already playing */
        if(IsPlaying(keyname,out var audioSource))
        {
            if (bInterrupt)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
            return;
        }

        if(IsPlaying(currentMusic,out var currentAudioSource) && currentMusic != "")
        {
            currentAudioSource.Stop();
            currentAudioSource.clip = null;
        }

        var soundPlaying = Play(keyname, Instance.Music);
        if(!soundPlaying)
        {
            return;
        }
        soundPlaying.loop = true;
        currentMusic = keyname;
    }

    private static void Stop(string keyname)
    {
        if(IsPlaying(keyname,out var audioSource))
        {
            audioSource.Stop();
        }
    }

    public static void PlaySFX(string keyname, bool bInterrupt,float volume = 0)
    {
        if (volume == 0)
        {
            volume = Instance.SFXVolume;
        }
        if (IsPlaying(keyname, out var audioSource))
        {
            if (bInterrupt)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }
        
        var src = Play(keyname, Instance.SFX);
        src.Stop();
        src.volume = volume / 100f;
        src.PlayOneShot(Instance.SFX[keyname]);
        //Instance.StartCoroutine(Instance.WaitForClipToEnd(src));
    }

    // Start is called before the first frame update


    #region SceneChange
    
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (EditorMusicVolume != MusicVolume)
            MusicVolume = EditorMusicVolume;
        if (EditorSFXVolume != SFXVolume)
            SFXVolume = EditorSFXVolume;
    }

    void OnValidate()
    {
        _musicKeyNames.Clear();
        _SFXKeyNames.Clear();

        #region List Music Files
      // Define the relative path to your Resources folder
        string musicPath = Path.Combine(Application.dataPath, "Resources\\Sounds\\Music");

        // Ensure the directory exists
        if (Directory.Exists(musicPath))
        {
            // Get all files in the directory
            string[] files = Directory.GetFiles(musicPath, "*.*")
                            .Where(file => !file.EndsWith(".meta"))
                            .ToArray();
            

            for(int i = 0;i < files.Length;i++)
            {

                //TODO: Create Functionality that will rename the file directily from the editor.
                // Extract the file name without the path or extension
                // if(_musicKeyNames[i] == files[i])
                // {
                //     continue;
                // }
                string fileName = Path.GetFileNameWithoutExtension(files[i]);
                _musicKeyNames.Add(fileName);
            }
        }
        else
        {
            Debug.LogWarning($"Directory does not exist: {musicPath}");
        }
        #endregion

            #region List Music Files
      // Define the relative path to your Resources folder
        string sfxPath = Path.Combine(Application.dataPath, "Resources\\Sounds\\SFX");

        // Ensure the directory exists
        if (Directory.Exists(sfxPath))
        {
            // Get all files in the directory
                        string[] files = Directory.GetFiles(sfxPath, "*.*")
                                       .Where(file => !file.EndsWith(".meta"))
                                       .ToArray();

            foreach (string filePath in files)
            {
                // Extract the file name without the path or extension
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                _SFXKeyNames.Add(fileName);
            }
        }
        else
        {
            Debug.LogWarning($"Directory does not exist: {sfxPath}");
        }
        #endregion

      
    }

    private IEnumerator WaitForClipToEnd(AudioSource source)
    {
        if (source.clip != null)
        {
            yield return new WaitForSeconds(source.clip.length - source.time);
            if (!source.isPlaying) // Ensure it wasn't restarted during the wait
            {
                source.clip = null;
            }
        }
    }


}
