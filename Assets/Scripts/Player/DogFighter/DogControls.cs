﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogControls : MonoBehaviour
{
    private float shotCounter;
    public float timeBetweenShots;
    
    float lastClicked = 0.0f;

    private Animator anim;
    private Rigidbody2D rb;

    public Vector2 jumpKickForce;
    public Vector2 forwardKickForce;

	public GameObject blast;
	public Transform blastPosition;

    private bool facingRight;

    private PlayerMovement playerMovement;

    void GetComponents()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Use this for initialization
    void Start ()
    {
        GetComponents();
	}


    // Update is called once per frame
    void Update ()
    {
        shotCounter -= Time.deltaTime;
        
        //---------------------------------------------------------------//
        // Light attacks

        // OneTwo Combo - L
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Time.time - lastClicked < 0.5)
            {
                KickComboFirstHit();
            }
            else
            {
                KickComboSecondHit();
            }
            lastClicked = Time.time;
        }

        // Flying Uppercut - W K
        if (Input.GetKeyDown(KeyCode.I))
        {
            JumpKick();
        }

        // Two Side Attack - S K
        if(Input.GetKeyDown(KeyCode.M))
        {

            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                StartCoroutine(KiBlast());
            }
        }

        // Spinning Kick - D K
        if (Input.GetKeyDown(KeyCode.K))
        {
            ForwardKick();
        }

        #if !UNITY_ANDROID && !UNITY_IPHONE && UNITY_BLACKBERRY && !UNITY_WINRT

        

        #endif

        // // Flying Uppercut - W J
        // if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.J))
        // {
            
        // }

        // // Two Side Attack - S J
        // if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.J))
        // {
            
        // }

        // // Spinning Kick - A J
        // if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.J))
        // {
            
        // }

        // // Spinning Kick - D J
        // if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.J))
        // {
            
        // }

        //---------------------------------------------------------------//
        // Heavy Attacks.

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    

        //}

 

    }



    void KickComboFirstHit()
    {
        anim.SetTrigger("KickCombo(0)");
    }

    void KickComboSecondHit()
    {
        anim.SetTrigger("KickCombo(1)");
    }

	void KickComboThirdHit()
    {
        anim.SetTrigger("KickCombo(2)");
    }

    void JumpKick()
    {
        facingRight = playerMovement.facingRight;
        anim.SetTrigger("JumpKick");

        if (facingRight)
        {
            if (jumpKickForce.x < 0f)
            {
                jumpKickForce.x *= -1;
            }
            rb.AddForce(jumpKickForce);
        }
        else
        {
            if (jumpKickForce.x > 0f)
            {
                jumpKickForce.x *= -1;
            }
            rb.AddForce(jumpKickForce);
        }
    }

    IEnumerator KiBlast()
    {
        anim.SetTrigger("KiBlast");

        yield return new WaitForSeconds(0.4f);
        GameObject newBlast = Instantiate(blast,blastPosition.position,Quaternion.identity);
        StopCoroutine(KiBlast());
    }

    void ForwardKick()
    {
        facingRight = playerMovement.facingRight;
        anim.SetTrigger("ForwardKick");

        if (facingRight)
        {
            if(forwardKickForce.x < 0f)
            {
                forwardKickForce.x *= -1;
            }
            rb.AddForce(forwardKickForce, ForceMode2D.Impulse);
        }
        else
        {
            if(forwardKickForce.x > 0f)
            {
                forwardKickForce.x *= -1;
            }
            rb.AddForce(forwardKickForce, ForceMode2D.Impulse);
        }

    }

}
