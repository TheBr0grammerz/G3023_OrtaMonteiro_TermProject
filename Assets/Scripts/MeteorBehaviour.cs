using System.Collections;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _minForce, _maxForce, _minTorque, _maxTorque;
    [SerializeField] private float _chanceToDrop;
    [SerializeField] private GameObject _droppedItemPrefab;
    [SerializeField] private SpriteRenderer _meteorSprite;

    private int _health = 10;

    void Start()
    {
        
    }

    void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        _meteorSprite.enabled = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(_minForce, _maxForce), Random.Range(_minForce, _maxForce)));
        rb.AddTorque(Random.Range(_minTorque, _maxTorque));

        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            _meteorSprite.enabled = false;
            GetComponentInChildren<Animator>().SetTrigger("MediumExplosion");
        }
        else
        {
            GetComponentInChildren<Animator>().SetTrigger("SmallExplosion");
        }
    }

    public void Destroy()
    {
        if (Random.value < _chanceToDrop)
        {
            GameObject.Instantiate(_droppedItemPrefab, transform.position, Quaternion.identity);
        }

        GameObject.Destroy(gameObject);
    }

}
