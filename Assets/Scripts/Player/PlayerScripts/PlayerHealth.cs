using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private string TAG_CHARACTER = "Character";
    private string TAG_BLAST = "Blast";

    private Animator anim;
    private Rigidbody2D rb;

    public float health = 0f;
    public Vector2 damage;
    public GameObject koPlayer;    

    private PlayerMovement playerMovement;
    
    private CatControls catControls;
    private DogControls dogControls;

    private SpriteRenderer spriteRenderer;
    private Color white = new Color(255,255,255,255);

    
    private Hit hit;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        
        if(gameObject.name == "CatFighter")
        {
            catControls = GetComponent<CatControls>();
        }

        if(gameObject.name == "DogFighter")
        {
            dogControls = GetComponent<DogControls>();
        }
	}

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == TAG_CHARACTER || collision.gameObject.tag == TAG_BLAST)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                HitTakenRight(collision.gameObject.GetComponentInParent<Hit>().GetDamage(),collision.gameObject.GetComponentInParent<Hit>().GetKnockback());
                koPlayer = collision.transform.parent.gameObject;
                DisableControls();
                spriteRenderer.material.color = white;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.material.color = Color.white;
                EnableControls();                
            }

            if (collision.transform.position.x < transform.position.x)
            {
                spriteRenderer.material.color = Color.white;
                
                HitTakenLeft(collision.gameObject.GetComponentInParent<Hit>().GetDamage(),collision.gameObject.GetComponentInParent<Hit>().GetKnockback());
                koPlayer = collision.transform.parent.gameObject;                
                DisableControls();
                spriteRenderer.material.color = white;                
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.material.color = Color.white;                
                EnableControls();
            }
        }
        

        
    }

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

    void HitTakenRight(float damage, Vector2 knockback)
    {
        anim.SetTrigger("Hit");
        health += damage;

        float x = (((((health/10) + ((health * damage)/20) * 2 * 1.4f) + 18) + 1.0f)*(health/10));
        Vector2 totalKnockback = new Vector2(-(x+knockback.x),(x+knockback.y));

        // Debug.Log(x);
        // Debug.Log("RIGHT : " + totalKnockback);
        
        rb.AddForce(totalKnockback);
    }

    void HitTakenLeft(float damage, Vector2 knockback)
    {
        anim.SetTrigger("Hit");
        health += damage;

        float x = (((((health/10) + ((health * damage)/20) * 2 * 1.4f) + 18) + 1.0f)*(health/10));
        Vector2 totalKnockback = new Vector2((x+knockback.x),(x+knockback.y));

        // Debug.Log(x);
        // Debug.Log("LEFT : " + totalKnockback);

        rb.AddForce(totalKnockback);
    }

    
}
