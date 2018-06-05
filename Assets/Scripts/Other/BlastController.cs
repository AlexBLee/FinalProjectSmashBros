using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody2D rBody;
    private PlayerMovement playerMovement;
    private Hit hit;
    public Vector2 knockback;

    

	// Use this for initialization
	void Start ()
    {
        playerMovement = GameObject.Find("DogFighter").GetComponent<PlayerMovement>();
        rBody = GetComponent<Rigidbody2D>();
        hit = GetComponent<Hit>();
        shootBullet();
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Shoot the bullet in the respective direction of the player.
    void shootBullet()
    {
        if (!playerMovement.facingRight)
        {
            rBody.velocity += (new Vector2(-100,0) * Time.deltaTime * speed);
        }
        else
        {
            rBody.velocity += (new Vector2(100,0) * Time.deltaTime * speed);
        }
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
