using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : MonoBehaviour 
{
	private string TAG_KILLZONE = "KillZone";
	private string TAG_PLATFORM = "Platform";

    public Platforms platforms;
	public Transform spawnPosition;

    private Rigidbody2D rb;
    public Animator anim;

    private PlayerMovement playerMovement;
    private DogControls dogControls;
    private CatControls catControls;

    private PlayerHealth playerHealth;
    public int lives = 5;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>(); 
        playerMovement = GetComponent<PlayerMovement>(); 
        rb = GetComponent<Rigidbody2D>();

        if(gameObject.name == "DogFighter")
            dogControls = GetComponent<DogControls>();

        if(gameObject.name == "CatFighter")
            catControls = GetComponent<CatControls>();


        Observable.EveryUpdate()
        .Where(_ => lives == 0)
        .Subscribe(_ =>
        {
            Destroy(gameObject);
        });

        
    }


	private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TAG_KILLZONE)
        {
            playerHealth.health = 0;
            --lives;
            rb.velocity = new Vector2(0,0);            
            yield return new WaitForSeconds(1);
            transform.position = spawnPosition.position;
            playerMovement.enabled = false;

            if(dogControls != null)
            {
                dogControls.enabled = false;
            }

            if(catControls != null)
            {
                catControls.enabled = false;
            }
            anim.SetTrigger("IsDead");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == TAG_PLATFORM)
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }

    IEnumerator OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == TAG_PLATFORM)
        {
            yield return new WaitForSeconds(1);
            transform.parent = null;
            playerMovement.enabled = true;
            if(dogControls != null)
            {
                dogControls.enabled = true;
            }
            if(catControls != null)
            {
                catControls.enabled = true;
            }

            if(platforms.done == true)
            {
                anim.SetTrigger("IsIdle");
                platforms.SetFalse();
            }

        }
    }

    
}

