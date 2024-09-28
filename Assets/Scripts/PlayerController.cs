using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;

    public SpriteRenderer[] FlameRenderers;

    public float speed;
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
