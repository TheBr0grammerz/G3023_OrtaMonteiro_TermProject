using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;

    public SpriteRenderer FlameRenderer;

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
            FlameRenderer.enabled = true;
        }
        else
        {
            FlameRenderer.enabled = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, player.transform.position, player.transform.rotation);
        }
    }
}
