using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    // Tags for objects
    private const string TAG_CHARACTER = "Character";
    private const string TAG_BLAST = "Blast";
    private const string TAG_DOG = "DogFighter";
    private const string TAG_CAT = "CatFighter";

    // Animation
    private Animator anim;

    // To apply force to the player.
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    // Health/Damage
    [SyncVar]
    public float health = 0f;
    private Hit hit;
    public Vector2 damage;
    public GameObject koPlayer;
    
    // Different characters.
    private CatControls catControls;
    private DogControls dogControls;

    // Flash the sprite when hit.
    private SpriteRenderer spriteRenderer;
    private Color white = new Color(255,255,255,255);

    // Audio
    private AudioSource source;
    public AudioClip clip;

    // --------------------------------------------------------------------------------------------------------- //    

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        source = GetComponent<AudioSource>();
        
        // Get the controls depending on the character chosen.
        if(gameObject.name == TAG_CAT)
        {
            catControls = GetComponent<CatControls>();
        }

        if(gameObject.name == TAG_DOG)
        {
            dogControls = GetComponent<DogControls>();
        }
	}

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == TAG_CHARACTER || collision.gameObject.tag == TAG_BLAST)
        {
            // If the player gets hit from the right.
            if (collision.transform.position.x > transform.position.x)
            {
                if(!isServer)
                {
                    CmdHitTaken(collision.gameObject.GetComponentInParent<Hit>().GetDamage(),collision.gameObject.GetComponentInParent<Hit>().GetKnockback(),-1);          
                }
                else
                {
                    RpcHitTaken(collision.gameObject.GetComponentInParent<Hit>().GetDamage(),collision.gameObject.GetComponentInParent<Hit>().GetKnockback(),-1);
                }
            }

            // If the player gets hit from the left.
            if (collision.transform.position.x < transform.position.x)
            {   
                if(!isServer)
                {
                    CmdHitTaken(collision.gameObject.GetComponentInParent<Hit>().GetDamage(),collision.gameObject.GetComponentInParent<Hit>().GetKnockback(),-1);          
                }
                else
                {
                    RpcHitTaken(collision.gameObject.GetComponentInParent<Hit>().GetDamage(),collision.gameObject.GetComponentInParent<Hit>().GetKnockback(),-1);
                }
            }

            // koPlayer marks the player that last hit you so the system is able to figure out who to give the kill to.
            if(collision.gameObject.tag == TAG_BLAST)
            {
                // NOTE: The reason koPlayer has an exception here on how it marks the player is because it is supposed to get the parent
                // of the collider, which is the player, however with the blast, there's no player interally, so there is a player marked in the blast.
                koPlayer = collision.gameObject.GetComponent<BlastController>().player;
            }
            else
            {
                // Get parent of collider.
                koPlayer = collision.transform.parent.gameObject;
            }
            
            // Play audio.
            source.PlayOneShot(clip);

            // Small stun, flash white and enable controls.
            DisableControls();
            spriteRenderer.material.color = white;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.material.color = Color.white;
            EnableControls();      
        }
        
    }

    // When you get hit, you'll be very briefly "stunned" (not be able to move or hit anything)
    public void DisableControls()
    {
        playerMovement.enabled = false;
        if(catControls != null)
        {
            catControls.enabled = false;
        }

        if(dogControls != null)
        {
            dogControls.enabled = false;
        }

    }

    // Re-enable controls after hit.
    public void EnableControls()
    {
        playerMovement.enabled = true;
        
        
        if(catControls != null)
        {
            catControls.enabled = true;
        }

        if(dogControls != null)
        {
            dogControls.enabled = true;            
        }
    }

    // When hit, determine damage, knockback and the direction that the player has been hit.
    // In terms of direction -- if they take damage from the right, it is -1, if left then 1.
    [Command]
    void CmdHitTaken(float damage, Vector2 knockback, int direction)
    {
        RpcHitTaken(damage,knockback,direction);
    }

    [ClientRpc]
    void RpcHitTaken(float damage, Vector2 knockback, int direction)
    {
        anim.SetTrigger("Hit");
        health += damage;

        float x = (((((health/10) + ((health * damage)/20) * 2 * 1.4f) + 18) + 1.0f)*(health/10));
        Vector2 totalKnockback = new Vector2(direction*(x+knockback.x),(x+knockback.y));
        
        rb.AddForce(totalKnockback);
    }

    
}
