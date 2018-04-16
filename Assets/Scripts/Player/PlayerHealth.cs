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
    private CatControls catControls;
    private DogControls dogControls;
    

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
                HitTakenRight();
                DisableControls();
                yield return new WaitForSeconds(0.1f);
                EnableControls();
                
                Debug.Log("hit right!");
            }

            if (collision.transform.position.x < transform.position.x)
            {
                HitTakenLeft();
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

    void HitTakenRight()
    {
        anim.SetTrigger("Hit");
        
        health += 10.0f;

        damage = new Vector2(health * -10, health * 10);

        
        Debug.Log(damage);

        rb.AddForce(damage);
    }

    void HitTakenLeft()
    {
        anim.SetTrigger("Hit");
        
        health += 10.0f;

        damage = new Vector2(health * 10, health * 10);

        Debug.Log(damage);

        rb.AddForce(damage);
    }
}
