using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBlastController : MonoBehaviour
{
    // To find the character shooting this.
    public GameObject player;

    // Speed of blast.
    private float speed = 5.0f;

    // Determine the direction of the player
    private Rigidbody2D rBody;
    private SPlayerMovement playerMovement;

    // For damage/knockback on player.
    private Hit hit;
    public Vector2 knockback;

    // --------------------------------------------------------------------------------------------------------- //


	void Start ()
    {
        // Only the dog fighter uses this, so finding only the DogFighter is appropriate.
        playerMovement = player.GetComponent<SPlayerMovement>();

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
            hit.SetDamage(10);
            hit.SetKnockback(knockback);
            Destroy(gameObject);
        }
    }
}
