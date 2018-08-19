using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0414


public class CatControls : NetworkBehaviour
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
    private PlayerMovement playerMovement;
    private bool facingRight;

    // Audio
    private AudioSource source;
    public AudioClip[] clips;

    // Mobile buttons
    MobileButtons mobileButtons;


    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        hit = GetComponent<Hit>();
        source = GetComponent<AudioSource>();

        #if UNITY_ANDROID
            mobileButtons = FindObjectOfType<MobileButtons>();
        #endif
	}

    void Update ()
    {

        if(!isLocalPlayer)
        {
            return;
        }

        
        if (Input.GetKeyDown(KeyCode.U) || (mobileButtons != null && mobileButtons.A))
        {
            if (Time.time - lastClicked < 0.5)
            {
                if(!isServer)
                    CmdOneTwo();
                else
                    RpcOneTwo();
            }
            else
            {
                if(!isServer)
                    CmdOneTwoSecond();
                else
                    RpcOneTwoSecond();
            }
            lastClicked = Time.time;

            if(mobileButtons != null)
            {
                mobileButtons.A = false;
            }
        }

        // Flying Uppercut
        if (Input.GetKeyDown(KeyCode.I) || (mobileButtons != null && mobileButtons.B))
        {
            if(!isServer)
                CmdFlyingUppercut();
            else
                RpcFlyingUppercut();

            if(mobileButtons != null)
            {
                mobileButtons.B = false;
            }

        }

        // Two Side Attack
        if(Input.GetKeyDown(KeyCode.O) || (mobileButtons != null && mobileButtons.C))
        {
            if(!isServer)
                CmdTwoSideAttack();
            else
                RpcTwoSideAttack();

            if(mobileButtons != null)
            {
                mobileButtons.C = false;
            }

        }

        // Spinning Kick
        if (Input.GetKeyDown(KeyCode.P) || (mobileButtons != null && mobileButtons.D))
        {
            if(!isServer)
                CmdSpinKick();
            else
                RpcSpinKick();

            if(mobileButtons != null)
            {
                mobileButtons.D = false;
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
        if(canUppercut && canHit)
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
            playerMovement.enabled = false;
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
    }

    // -----------------------------------------------------------------------------------
    // For networking.

    [Command]
    void CmdOneTwo()
    {
        RpcOneTwo();
    }

    [ClientRpc]
    void RpcOneTwo()
    {
        OneTwoComboFirstHit();
    }

     [Command]
    void CmdOneTwoSecond()
    {
        RpcOneTwoSecond();
    }

    [ClientRpc]
    void RpcOneTwoSecond()
    {
        OneTwoComboSecondHit();
    }

    // -----------------------------------------------------------------------------------


    [Command]
    void CmdFlyingUppercut()
    {
        RpcFlyingUppercut();
    }

    [ClientRpc]
    void RpcFlyingUppercut()
    {
        FlyingUppercut();
    }
    // -----------------------------------------------------------------------------------


    [Command]
    void CmdTwoSideAttack()
    {
        RpcTwoSideAttack();
    }

    [ClientRpc]
    void RpcTwoSideAttack()
    {
        TwoSideAttack();
    }
    // -----------------------------------------------------------------------------------


    [Command]
    void CmdSpinKick()
    {
        RpcSpinKick();
    }

    [ClientRpc]
    void RpcSpinKick()
    {
        SpinKick();
    }




}
