﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0414


public class SCatControls : MonoBehaviour
{
    // To determine when you can hit and when you clicked last.
    private bool canHit = true;
    private bool canKick;
    private bool canUppercut = true;
    private float lastClicked = 0.0f;

    // Animation
    public Animator anim;

    // To detect the floor.
    private string TAG_FLOOR = "Floor";

    // For move forces.
    private Rigidbody2D rb;

    // Move forces
    public Vector2 uppercutForce;
    public Vector2 sideKickForceR;

    // Move knockbacks.
    public Vector2 punchKnockback;
    public Vector2 uppercutKnockback;
    public Vector2 twoSideKnockback;
    public Vector2 spinKickKnockback;

    // Sets Damage and Knockback
    private Hit hit;
    private SPlayerMovement playerMovement;
    private bool facingRight;

    // Audio
    public AudioSource source;
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
        // If player 1
        if(playerMovement.player1)
        {
            // Two Punch Combo
            if (Input.GetKeyDown(KeyCode.J))
            {
                OneTwoComboFirstHit();
                
                if (Time.time - lastClicked < 0.5)
                {
                    OneTwoComboSecondHit();
                }

                lastClicked = Time.time;
            }

            // Flying Uppercut
            if (Input.GetKeyDown(KeyCode.I))
            {
                FlyingUppercut();
            }

            // Two Side Attack
            if(Input.GetKeyDown(KeyCode.K))
            {
                TwoSideAttack();
            }

            // Spinning Kick
            if (Input.GetKeyDown(KeyCode.L))
            {
                SpinKick();
            }
        }

        // If player 2
        if(playerMovement.player2)
        {
            // Two Punch Combo
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                if (Time.time - lastClicked < 0.5)
                {
                    OneTwoComboSecondHit();
                }
                else
                {
                    OneTwoComboFirstHit();
                }
                lastClicked = Time.time;
            }

            // Flying Uppercut
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                FlyingUppercut();
            }

            // Two Side Attack
            if(Input.GetKeyDown(KeyCode.Keypad2))
            {
                TwoSideAttack();
            }

            // Spinning Kick
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                SpinKick();
            }
        }
    }

    // --------------------------------------------------------------------------------------------------------- //
    // MOVES
    // --------------------------------------------------------------------------------------------------------- //

    // Two punch combo
    void OneTwoComboFirstHit()
    {
        anim.SetTrigger("1-2Combo(1)");
        hit.SetDamage(4);
        hit.SetKnockback(punchKnockback);
        hit.isBasicAttack = true;
        source.PlayOneShot(clips[0]);


    }

    void OneTwoComboSecondHit()
    {
        anim.SetTrigger("1-2Combo(2)");
        hit.SetDamage(4);
        hit.SetKnockback(punchKnockback);
        hit.isBasicAttack = true;
        source.PlayOneShot(clips[0]);

    }

    // --------------------------------------------------------------------------------------------------------- //

    public void FlyingUppercut()
    {
        if(canUppercut)
        {
            facingRight = playerMovement.facingRight;
            anim.SetTrigger("SpinUppercut");
            
            // If the player is facing right, and the force for the move is still negative, then change it to positive.            
            if (facingRight)
            {
                if (uppercutForce.x < 0f)
                {
                    uppercutForce.x *= -1;
                }
                rb.AddForce(uppercutForce);
            }
            else
            {
                if (uppercutForce.x > 0f)
                {
                    uppercutForce.x *= -1;
                }
                rb.AddForce(uppercutForce);
            }
            source.PlayOneShot(clips[1]);
            canHit = false;
            canUppercut = false;
        }

        hit.SetDamage(10);
        hit.SetKnockback(uppercutKnockback);
        hit.isBasicAttack = false;

        
    }

    // --------------------------------------------------------------------------------------------------------- //

    public void TwoSideAttack()
    {
        if(canHit)
        {
            source.PlayOneShot(clips[0]);
            anim.SetTrigger("TwoSide");
            
            hit.SetDamage(5);
            hit.SetKnockback(twoSideKnockback);
            hit.isBasicAttack = false;

        }
    }

    // --------------------------------------------------------------------------------------------------------- //

    public void SpinKick()
    {
        if(canHit)
        {
            facingRight = playerMovement.facingRight;

            anim.SetTrigger("SpinKick");

            if (facingRight)
            {
                if(sideKickForceR.x < 0f)
                {
                    sideKickForceR.x *= -1;
                }
                rb.AddForce(sideKickForceR);
            }
            else
            {
                if(sideKickForceR.x > 0f)
                {
                    sideKickForceR.x *= -1;
                }
                rb.AddForce(sideKickForceR);
            }
            canHit = false;
        }
        source.PlayOneShot(clips[2]);
        hit.SetDamage(8);
        hit.SetKnockback(spinKickKnockback);
        hit.isBasicAttack = false;


    }

    // Check if animation is done. -- Called in animation.
    public void SpinKickCheck()
    {
        canKick = true;
    }

    // --------------------------------------------------------------------------------------------------------- //


    // When the player lands, enable you to uppercut again.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TAG_FLOOR)
        {
            canUppercut = true;
        }
    }

    public void EnableCatControl()
    {
        playerMovement.enabled = true;
        canHit = true;
        canKick = true;
        canUppercut = true;
    }

    public void EnableCatControlU()
    {
        playerMovement.enabled = true;
        canHit = true;
        canKick = true;
    }

}
