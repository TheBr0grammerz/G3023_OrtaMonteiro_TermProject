using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSliderAdjuster : MonoBehaviour
{
    [SerializeField] public SoundType soundType;
    public void AdjustSound(float volume)
    {

        switch(soundType)
        {
            case SoundType.Music:
                SoundManager.Instance.EditorMusicVolume = volume;
                break;
            case SoundType.SFX:
                SoundManager.Instance.EditorSFXVolume = volume;
                break;
        }
        Debug.Log("Volume: " + volume);
    }

    public void UpdateSliderValue()
    {
        switch(soundType)
        {
            case SoundType.Music:
                GetComponent<UnityEngine.UI.Slider>().value = SoundManager.Instance.MusicVolume;
                break;
            case SoundType.SFX:
                GetComponent<UnityEngine.UI.Slider>().value = SoundManager.Instance.SFXVolume;
                break;
        }
    }
    // Start is called before the first frame update
    void  Awake()
    {
        GetComponent<UnityEngine.UI.Slider>().maxValue = 100;
    }


    void OnEnable()
    {
        GetComponent<UnityEngine.UI.Slider>().onValueChanged.AddListener(AdjustSound);
        UpdateSliderValue();
    }
    void OnDisable()
    {
        GetComponent<UnityEngine.UI.Slider>().onValueChanged.RemoveListener(AdjustSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public enum SoundType
{
    Music,
    SFX
}
