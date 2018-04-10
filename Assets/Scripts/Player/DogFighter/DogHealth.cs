using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHealth : MonoBehaviour
{
    private string TAG_CHARACTER = "Character";
    private string TAG_BLAST = "Blast";

    private Animator anim;
    private Rigidbody2D rb;

    public float health = 0f;
    public Vector2 damage;

    private PlayerMovement playerMovement;
    private DogControls dogControls;
    

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        dogControls = GetComponent<DogControls>();
	}

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == TAG_CHARACTER || collision.gameObject.tag == TAG_BLAST)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                HitTakenRight();
                anim.SetTrigger("Hit");
                playerMovement.enabled = false;
                dogControls.enabled = false;
                yield return new WaitForSeconds(0.1f);
                playerMovement.enabled = true;
                dogControls.enabled = true;
                
                Debug.Log("hit right!");
            }

            if (collision.transform.position.x < transform.position.x)
            {
                HitTakenLeft();
                anim.SetTrigger("Hit");
                playerMovement.enabled = false;
                dogControls.enabled = false;
                yield return new WaitForSeconds(0.1f);
                playerMovement.enabled = true;
                dogControls.enabled = true;
            }
        }
        

        
    }

    void HitTakenRight()
    {
        health += 10.0f;

        damage = new Vector2(health * -10, health * -10);

        
        Debug.Log(damage);

        rb.AddForce(damage);
    }

    void HitTakenLeft()
    {
        health += 10.0f;

        damage = new Vector2(health * 10, health * 10);

        Debug.Log(damage);

        rb.AddForce(damage);
    }
}
