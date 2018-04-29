using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    public float speed = 0.5f;
    private PlayerMovement playerMovement;
    public Vector2 knockback;
    private Rigidbody2D rBody;
    private Hit hit;
    

	// Use this for initialization
	void Start ()
    {
        playerMovement = GameObject.Find("DogFighter").GetComponent<PlayerMovement>();
        rBody = GetComponent<Rigidbody2D>();
        hit = GetComponent<Hit>();
        shootBullet();

        
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void shootBullet()
    {
        if (playerMovement.facingRight != true)
            rBody.velocity += (new Vector2(-100,0) * Time.deltaTime * speed);
        else
            rBody.velocity += (new Vector2(100,0) * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            hit.SetDamage(3);
            hit.SetKnockback(knockback);
            Destroy(gameObject);
        }
    }
}
