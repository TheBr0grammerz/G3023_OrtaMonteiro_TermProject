using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    

    void Start()
    {
        GameObject.Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Meteor"))
        {
            MeteorBehaviour mb = collider.gameObject.GetComponent<MeteorBehaviour>();
            mb.TakeDamage(_damage);

            GameObject.Destroy(gameObject);
        }
    }
}
