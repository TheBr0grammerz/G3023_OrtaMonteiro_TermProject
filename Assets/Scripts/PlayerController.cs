using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private SpriteRenderer FlameRenderer;

    public float speed;
    public float rotationSpeed;


    void Start()
    {
        
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
