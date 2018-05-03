using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControls : MonoBehaviour
{
    bool canHit = true;
    float lastClicked = 0.0f;

    public Animator anim;
    private Rigidbody2D rb;
    private bool canUppercut = true;
    private bool canKick = true;

    public Vector2 uppercutForce;
    public Vector2 sideKickForceR;

    private string TAG_FLOOR = "Floor";

    private bool facingRight;

    public Vector2 uppercutKnockback;
    public Vector2 spinKickKnockback;
    public Vector2 twoSideKnockback;
    public Vector2 punchKnockback;

    private Hit hit;

    private PlayerMovement playerMovement;

    void GetComponents()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        hit = GetComponent<Hit>();
    }

    // Use this for initialization
    void Start ()
    {
        GetComponents();
        
	}


    // Update is called once per frame
    void Update ()
    {
        //---------------------------------------------------------------//
        // Light attacks

        // OneTwo Combo - L
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

        //---------------------------------------------------------------//
        // Heavy Attacks.

        // Flying Uppercut - W K
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            FlyingUppercut();
        }

        // Two Side Attack - S K
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            TwoSideAttack();
        }

        // Spinning Kick - D K
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SpinKick();
        }

    }

    public void OneTwoCombo()
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

    void OneTwoComboFirstHit()
    {
        anim.SetTrigger("1-2Combo(1)");
        hit.SetDamage(4);
        hit.SetKnockback(punchKnockback);
    }

    void OneTwoComboSecondHit()
    {
        anim.SetTrigger("1-2Combo(2)");
        hit.SetDamage(4);
        hit.SetKnockback(punchKnockback);        
    }

    public void FlyingUppercut()
    {
        if(canUppercut && canHit)
        {
            facingRight = playerMovement.facingRight;
            anim.SetTrigger("SpinUppercut");
            

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
            playerMovement.enabled = false;            
            canHit = false;
            canUppercut = false;
        }

        hit.SetDamage(10);
        hit.SetKnockback(uppercutKnockback);
        
    }

    public void TwoSideAttack()
    {
        if(canHit)
        {
            playerMovement.enabled = false;
            anim.SetTrigger("TwoSide");
            hit.SetDamage(5);
            hit.SetKnockback(twoSideKnockback);
        }
    }

    public void SpinKick()
    {
        if(canKick && canHit)
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
            playerMovement.enabled = false;
            canHit = false;
            canKick = false;
        }

        hit.SetDamage(8);
        hit.SetKnockback(spinKickKnockback);

    }

    public void SpinKickCheck()
    {
        canKick = true;
    }

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
    }

}
