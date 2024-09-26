using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField] 
    private SpriteRenderer[] _backgroundLayers;

    public float _distance = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }

        _distance = GetDistanceToCenter();

        ChangeBackground();

    }

    private float GetDistanceToCenter()
    {
       return Vector3.Distance(transform.position, Vector3.zero);
    }

    private void ChangeBackground()
    {
        float distance = GetDistanceToCenter();
        float ratio = Mathf.Clamp(distance/100f, 0f, 1f);

        _backgroundLayers[0].color = new Color(_backgroundLayers[0].color.r, _backgroundLayers[0].color.g, 
            _backgroundLayers[0].color.b, Mathf.Lerp(255f, 0f, ratio));
    }

}
