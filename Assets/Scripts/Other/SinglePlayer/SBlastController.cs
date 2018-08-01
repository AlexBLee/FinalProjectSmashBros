using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBlastController : MonoBehaviour
{
    // Speed of blast.
    private float speed = 5.0f;

    // Determine the direction of the player
    private Rigidbody2D rBody;
    private SPlayerMovement playerMovement;

    // For damage/knockback on player.
    private Hit hit;
    public Vector2 knockback;
    public GameObject player;

    // --------------------------------------------------------------------------------------------------------- //


	void Start ()
    {
        // Only the dog fighter uses this, so finding only the DogFighter is appropriate.
        playerMovement = GameObject.Find("SDogFighter").GetComponent<SPlayerMovement>();

        rBody = GetComponent<Rigidbody2D>();
        hit = GetComponent<Hit>();
        shootBullet();
	}

    // Destroy self when you can't see it.
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

    // Hit people.
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
