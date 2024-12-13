using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField] 
    private float _effectMultiplier;

    private Vector2 _length;
    private Vector3 _startPosition;


    void Start()
    {
        _startPosition = transform.position;

        if (GetComponent<SpriteRenderer>() != null)
        {
            _length.x = GetComponent<SpriteRenderer>().bounds.size.x;
            _length.y = GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else if (GetComponentInChildren<SpriteRenderer>() != null)
        {
            _length.x = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
            _length.y = GetComponentInChildren<SpriteRenderer>().bounds.size.y;
        }
    }

    void Update()
    {
        ApplyEffect(_effectMultiplier);
    }

    private void ApplyEffect(float effectMultiplier)
    {

        Vector2 tempPosition = new Vector2(_camera.transform.position.x * (1f - effectMultiplier),
            _camera.transform.position.y * (1f - effectMultiplier));

        // calculate displacement from parallax effect
        Vector2 distance = new Vector2(_camera.transform.position.x * effectMultiplier,
            _camera.transform.position.y * effectMultiplier);

        // set position to 
        transform.position = new Vector3(_startPosition.x + distance.x, _startPosition.y + distance.y, 0);

        // repeat background
        if (tempPosition.x > _startPosition.x + _length.x)
            _startPosition.x += _length.x;
        else if (tempPosition.x < _startPosition.x - _length.x)
            _startPosition.x -= _length.x;

        if (tempPosition.y > _startPosition.y + _length.y)
            _startPosition.y += _length.y;
        else if (tempPosition.y < _startPosition.y - _length.y)
            _startPosition.y -= _length.y;
    }
}
