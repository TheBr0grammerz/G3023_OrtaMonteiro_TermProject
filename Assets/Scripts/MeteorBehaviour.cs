using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _minForce, _maxForce, _minTorque, _maxTorque;

    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.AddForce(new Vector2(Random.Range(_minForce, _maxForce), Random.Range(_minForce, _maxForce)));
        _rigidbody.AddTorque(Random.Range(_minTorque, _maxTorque));
    }

}
