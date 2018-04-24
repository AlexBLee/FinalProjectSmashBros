﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	private string TAG_KILLZONE = "KillZone";
	private string TAG_PLATFORM = "Platform";

    public Platforms platforms;
	public Transform spawnPosition;

    private Rigidbody2D rb;
    public Animator anim;

    private PlayerHealth playerHealth;
    public int lives = 5;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();   
        rb = GetComponent<Rigidbody2D>();     
    }

    void Update()
    {
        if(lives <= 0)
        {
            Destroy(gameObject);
        }
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

            if(platforms.done == true)
            {
                anim.SetTrigger("IsIdle");
                platforms.SetFalse();
            }

        }
    }
}
