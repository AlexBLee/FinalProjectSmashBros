using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    int clickCounter = 0;
    float comboCounter = 1.0f;

    private float timeBetweenClicks;
    bool buttonPressed = false;

    public float tapSpeed = 1.0f;
    private Animator anim;
    private Rigidbody2D rb;

    public Vector2 uppercutForce;

    public Vector2 sideKickForceR;
    public Vector2 sideKickForceL;


    private int clickCount = 0;
    private float timer = 0;



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

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    timeBetweenClicks = Time.time;
        //    StartCoroutine(comboTimer());
        //    ++clickCounter;
        //    OneTwoCombo();
        //    StopCoroutine(comboTimer());

        //}

        if (Input.GetKeyDown(KeyCode.L))
        {
            OneTwoCombo();
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            FlyingUppercut();
        }

        // Two Side Attack - S K
        if(Input.GetKeyDown(KeyCode.M))
        {
            TwoSideAttack();
        }

        // Spinning Kick - A K
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpinKickLeft();
        }

        // Spinning Kick - D K
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpinKickRight();
        }

    }

    void GetComponents()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OneTwoCombo()
    {
            
            if (clickCount == 1)//Number of tabs you want minus one
            {
                anim.SetTrigger("1-2Combo(1)");
            }

            if (timer > 0 && clickCount == 2)//Number of tabs you want minus one
            {
                anim.SetTrigger("1-2Combo(2)");
            }

            else
            {
                timer = 0.5f;
                clickCount += 1;
            }


        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        else
        {
            clickCount = 0;
        }
    }

    void FlyingUppercut()
    {
        anim.SetTrigger("SpinUppercut");
        rb.AddForce(uppercutForce);
    }

    void TwoSideAttack()
    {
        anim.SetTrigger("TwoSide");
    }

    void SpinKickLeft()
    {
        anim.SetTrigger("SpinKick");
        rb.AddForce(sideKickForceL);
    }

    void SpinKickRight()
    {
        anim.SetTrigger("SpinKick");
        rb.AddForce(sideKickForceR);
    }

    //IEnumerator ComboTimer()
    //{
    //    //yield return new WaitForSeconds(1.0f);
    //    //clickCounter = 0;
    //    //anim.SetBool("OneTwoCombo1", false);
    //    //anim.SetBool("OneTwoCombo2", false);


    //}
}
