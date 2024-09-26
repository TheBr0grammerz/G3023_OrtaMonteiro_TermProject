using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField] 
    private SpriteRenderer[] _bgLayers;

    [SerializeField] 
    private float[] _distanceMarkers;

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

        ChangeBackground();
    }

    private float GetDistanceToCenter()
    {
       return Vector3.Distance(transform.position, Vector3.zero);
    }

    private void ChangeBackground()
    {
        float distance = GetDistanceToCenter();

        if (distance <= _distanceMarkers[0])
        {
            float a = Mathf.InverseLerp(_distanceMarkers[0], 0, distance);
            _bgLayers[0].color = new Color(1f, 1f, 1f, a);
        }
        else if (distance <= _distanceMarkers[1])
        {
            float a = Mathf.InverseLerp(_distanceMarkers[1], _distanceMarkers[0], distance);
            _bgLayers[1].color = new Color(1f, 1f, 1f, a);
        }
        else if (distance <= _distanceMarkers[2])
        {
            float a = Mathf.InverseLerp(_distanceMarkers[2], _distanceMarkers[1], distance);
            _bgLayers[2].color = new Color(1f, 1f, 1f, a);
        }
    }

}
