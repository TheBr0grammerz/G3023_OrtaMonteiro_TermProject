using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField] 
    private SpriteRenderer[] _bgLayer1, _bgLayer2, _bgLayer3;

    [SerializeField] 
    private float[] _distanceMarkers;

    private float _distanceToCenter, _length;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        _length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            _distanceToCenter = _player.GetDistanceToCenter();
            ParallaxEffect(0.9f);
            TransitionBackground();
        }

    }

    private void TransitionBackground()
    {

        if (_distanceToCenter <= _distanceMarkers[0])
        {
            float a = Mathf.InverseLerp(_distanceMarkers[0], 0, _distanceToCenter);

            for (int i = 0; i < _bgLayer1.Length; i++)
            {
                _bgLayer1[i].color = new Color(1f, 1f, 1f, a);
            }
        }
        else if (_distanceToCenter <= _distanceMarkers[1])
        {
            float a = Mathf.InverseLerp(_distanceMarkers[1], _distanceMarkers[0], _distanceToCenter);

            for (int i = 0; i < _bgLayer2.Length; i++)
            {
                _bgLayer2[i].color = new Color(1f, 1f, 1f, a);
            }
        }
        else if (_distanceToCenter <= _distanceMarkers[2])
        {
            float a = Mathf.InverseLerp(_distanceMarkers[2], _distanceMarkers[1], _distanceToCenter);

            for (int i = 0; i < _bgLayer3.Length; i++)
            {
                _bgLayer3[i].color = new Color(1f, 1f, 1f, a);
            }
        }
    }

    private void ParallaxEffect(float effectMultiplier)
    {
        Vector2 tempPosition = new Vector2(_player.transform.position.x * (1f - effectMultiplier), 
            _player.transform.position.y * (1f - effectMultiplier));
        Vector2 distance = new Vector2(_player.transform.position.x * effectMultiplier,
            _player.transform.position.y * effectMultiplier);

        // set position to 
        transform.position = new Vector3(_startPosition.x + distance.x, _startPosition.y + distance.y, 0);

        // repeat background
        if (tempPosition.x > _startPosition.x + _length) 
            _startPosition.x += _length;
        else if (tempPosition.x < _startPosition.x - _length) 
            _startPosition.x -= _length;

        if (tempPosition.y > _startPosition.y + _length) 
            _startPosition.y += _length;
        else if (tempPosition.y < _startPosition.y - _length) 
            _startPosition.y -= _length;
    }

}
