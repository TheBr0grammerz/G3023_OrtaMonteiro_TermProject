using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public SpriteRenderer[] FlameRenderers;
    
    [SerializeField]
    private GameObject projectile;

    public float speed;
    public float rotationSpeed;


    void Start()
    {
        Ship ship = player.GetComponent<Ship>();
        
        
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
            player.transform.Rotate(Input.GetAxis("Horizontal") * new Vector3(0,0,-1) * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Vertical"))
        {
            player.GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * speed * Time.deltaTime * player.transform.up);
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
            Instantiate(projectile, player.transform.position, player.transform.rotation);
        }
    }
}
