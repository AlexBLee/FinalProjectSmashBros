﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDogControls : MonoBehaviour
{
    // To determine when you can hit and when you clicked last.
    bool canHit = true;
    bool canJumpKick = true;
    bool thirdKick = false;
    float lastClicked = 0.0f;
    private float shotCounter;
    public float timeBetweenShots;
    
    // Animation
    private Animator anim;

    // To detect the floor.
    string TAG_FLOOR = "Floor";
    
    // For move forces.
    private Rigidbody2D rb;
    
    // Move forces
    public Vector2 jumpKickForce;
    public Vector2 forwardKickForce;

    // Move knockbacks.
    public Vector2 jumpKickKnockback;
    public Vector2 forwardKickKnockback;
    public Vector2 kickKnockback;

    // Sets Damage and Knockback
    private Hit hit;
    private SPlayerMovement playerMovement;
    private bool facingRight;
    
    // For the blast
	public GameObject blast;
	public Transform blastPosition;
    
    // Audio
    private AudioSource source;
    public AudioClip[] clips;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<SPlayerMovement>();
        hit = GetComponent<Hit>();
        source = GetComponent<AudioSource>();
	}


    void Update ()
    {
        // So you can't spam blast.
        shotCounter -= Time.deltaTime;

        // If player 1
        if(playerMovement.player1)
        {
             // Two Kick Combo
            if (Input.GetKeyDown(KeyCode.J))
            {
                thirdKick = false;
                KickComboFirstHit();
                
                if (Time.time - lastClicked < 0.5 && thirdKick == false)
                {
                    KickComboSecondHit();
                    thirdKick = true;              
                }

                if(Time.time - lastClicked < 0.5 && thirdKick == true)
                {
                    KickComboThirdHit();                
                    thirdKick = false;

                }

                lastClicked = Time.time;
            }

            // Flying Kick
            if (Input.GetKeyDown(KeyCode.I))
            {
                JumpKick();
            }

            // Ki blast
            if(Input.GetKeyDown(KeyCode.K))
            {
                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    KiBlast();
                }
            }

            // Forward Kick
            if (Input.GetKeyDown(KeyCode.L))
            {
                ForwardKick();
            }

        }

        // If player 2
        if(playerMovement.player2)
        {
             // Two Kick Combo
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                thirdKick = false;
                KickComboFirstHit();
                
                if (Time.time - lastClicked < 0.5 && thirdKick == false)
                {
                    KickComboSecondHit();
                    thirdKick = true;              
                }

                if(Time.time - lastClicked < 0.5 && thirdKick == true)
                {
                    KickComboThirdHit();                
                    thirdKick = false;

                }

                lastClicked = Time.time;
            }

            // Flying Kick
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                JumpKick();
            }

            // Ki Blast
            if(Input.GetKeyDown(KeyCode.Keypad2))
            {
                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    anim.SetTrigger("KiBlast");
                }
            }

            // Forward Kick
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                ForwardKick();
            }

        }

    }

    // --------------------------------------------------------------------------------------------------------- //
    // MOVES
    // --------------------------------------------------------------------------------------------------------- //

    // Kick combo 
    public void KickComboFirstHit()
    {
        anim.SetTrigger("KickCombo(0)");
        hit.SetDamage(4);
        hit.SetKnockback(kickKnockback);
        hit.isBasicAttack = true;
        source.PlayOneShot(clips[0]);
    }

    public void KickComboSecondHit()
    {
        anim.SetTrigger("KickCombo(1)");
        hit.SetDamage(4);
        hit.SetKnockback(kickKnockback);
        hit.isBasicAttack = true;
        source.PlayOneShot(clips[0]);

    }

	public void KickComboThirdHit()
    {
        anim.SetTrigger("KickCombo(2)");
        hit.SetDamage(4);
        hit.SetKnockback(kickKnockback);
        source.PlayOneShot(clips[0]);  
    }

    // --------------------------------------------------------------------------------------------------------- //

    public void JumpKick()
    {
        if(canJumpKick && canHit)
        {
            facingRight = playerMovement.facingRight;
            anim.SetTrigger("JumpKick");
            
            // If the player is facing right, and the force for the move is still negative, then change it to positive.
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
            canHit = false;
            canJumpKick = false;
            source.PlayOneShot(clips[1]);

            
        }
        hit.SetDamage(10);
        hit.SetKnockback(jumpKickKnockback);
        hit.isBasicAttack = false;

         
    }

    // --------------------------------------------------------------------------------------------------------- //
    
    void KiBlast()
    {
        if(canHit)
        {
            // anim.SetTrigger("KiBlast");
        
            GameObject ball = Instantiate(blast,blastPosition.position,Quaternion.identity);
            ball.GetComponent<SBlastController>().player = gameObject;
            
            source.PlayOneShot(clips[2]);
            
        }

    }

    // --------------------------------------------------------------------------------------------------------- //

    public void ForwardKick()
    {
        if(canHit)
        {
            facingRight = playerMovement.facingRight;
            anim.SetTrigger("ForwardKick");

            if (facingRight)
            {
                if(forwardKickForce.x < 0f)
                {
                    forwardKickForce.x *= -1;
                }
                rb.AddForce(forwardKickForce);
            }
            else
            {
                if(forwardKickForce.x > 0f)
                {
                    forwardKickForce.x *= -1;
                }
                rb.AddForce(forwardKickForce);
            }
            canHit = false;
            source.PlayOneShot(clips[0]);

        }
        hit.SetDamage(5);
        hit.SetKnockback(forwardKickKnockback);
        hit.isBasicAttack = false;

        

    }

    // --------------------------------------------------------------------------------------------------------- //
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TAG_FLOOR)
        {
            canJumpKick = true;
        }
    }


    public void EnableDogControl()
    {
        playerMovement.enabled = true;
        canHit = true;
    }

}
