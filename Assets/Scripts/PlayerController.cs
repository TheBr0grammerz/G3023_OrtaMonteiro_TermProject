using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private SpriteRenderer[] FlameRenderers;
    [SerializeField] private TextMeshProUGUI _promptText;



    void Start()
    {
        _player = GetComponent<Player>();
        Ship ship = _player.GetComponent<Ship>();

        var weapon = Resources.Load("Weapons/Tier 1 Laser") as BaseWeapon;
        var slot = new WeaponSlot(weapon);
        ship.weapons.Add(slot);
        
         weapon = Resources.Load("Weapons/Tier 1 Missile") as BaseWeapon;
         slot = new WeaponSlot(weapon);
        ship.weapons.Add(slot);
        Debug.Log("Adding To ships Weapons In Player Controller");
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.Interact();
        }
    }

    public void SetPromptText(string text)
    {
        _promptText.text = text;
    }
}
