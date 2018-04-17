using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControls : MonoBehaviour
{

    float lastClicked = 0.0f;

    public Animator anim;
    private Rigidbody2D rb;
    private bool canUppercut = true;
    private bool canKick = true;

    public Vector2 uppercutForce;
    public Vector2 sideKickForceR;

    private string TAG_FLOOR = "Floor";

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
        //---------------------------------------------------------------//
        // Light attacks

        // OneTwo Combo - L
        if (Input.GetKeyDown(KeyCode.Keypad4))
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

        // Flying Uppercut - W J
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.J))
        {
            
        }

        // Two Side Attack - S J
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.J))
        {
            
        }

        // Spinning Kick - A J
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.J))
        {
            
        }

        // Spinning Kick - D J
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.J))
        {
            
        }

        //---------------------------------------------------------------//
        // Heavy Attacks.

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    

        //}

        // Flying Uppercut - W K
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            FlyingUppercut();
        }

        // Two Side Attack - S K
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            TwoSideAttack();
        }

        // Spinning Kick - D K
        if (Input.GetKeyDown(KeyCode.Keypad2))
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
    }

    void OneTwoComboSecondHit()
    {
        anim.SetTrigger("1-2Combo(2)");
    }

    public void FlyingUppercut()
    {
        if(canUppercut)
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

            canUppercut = false;
        }
        
    }

    public void TwoSideAttack()
    {
        anim.SetTrigger("TwoSide");
    }

    public void SpinKick()
    {
        if(canKick)
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

            canKick = false;
        }

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
}
