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

    private PlayerMovement playerMovement;
    private PlayerMovement2 playerMovement2;
    
    private CatControls catControls;
    private DogControls dogControls;
    
    private Hit hit;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                DisableControls();
                yield return new WaitForSeconds(0.1f);
                EnableControls();                
            }

            if (collision.transform.position.x < transform.position.x)
            {
                HitTakenLeft(collision.gameObject.GetComponentInParent<Hit>().GetDamage(),collision.gameObject.GetComponentInParent<Hit>().GetKnockback());
                DisableControls();
                yield return new WaitForSeconds(0.1f);
                EnableControls();
            }
        }
        

        
    }

    void DisableControls()
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

    void EnableControls()
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

        Debug.Log(x);
        Debug.Log("RIGHT : " + totalKnockback);
        
        rb.AddForce(totalKnockback);
    }

    void HitTakenLeft(float damage, Vector2 knockback)
    {
        anim.SetTrigger("Hit");
        health += damage;

        float x = (((((health/10) + ((health * damage)/20) * 2 * 1.4f) + 18) + 1.0f)*(health/10));
        Vector2 totalKnockback = new Vector2((x+knockback.x),(x+knockback.y));
        Debug.Log(x);
        Debug.Log("LEFT : " + totalKnockback);
        rb.AddForce(totalKnockback);
    }

    
}
