using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Player _player;

    [SerializeField]
    private SpriteRenderer[] FlameRenderers;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            _player.transform.Rotate(Input.GetAxis("Horizontal") * new Vector3(0,0,-1) * _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Vertical"))
        {
            _player.GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * _speed * Time.deltaTime * _player.transform.up);
            foreach (var flame in FlameRenderers)
            {
                flame.enabled = true;
            }
        }
        else
        {
            foreach (var flame in FlameRenderers)
            {
                flame.enabled = false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _player.Shoot();
        }
    }
}
