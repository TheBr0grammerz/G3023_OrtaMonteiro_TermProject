using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField] 
    private SpriteRenderer[] _bgLayer1, _bgLayer2, _bgLayer3;

    [SerializeField] 
    private float[] _distanceMarkers;

    private float _distanceToCenter;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            _distanceToCenter = _player.GetDistanceToCenter();
            TransitionBackground();
        }

    }

    private void TransitionBackground()
    {
        // change alpha values based on distance to center

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
}
