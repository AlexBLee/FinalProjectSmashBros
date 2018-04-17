using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogControls : MonoBehaviour
{
    bool canJumpKick = true;
    bool canKick = true;
    string TAG_FLOOR = "Floor";
    private float shotCounter;
    public float timeBetweenShots;
    
    float lastClicked = 0.0f;

    private Animator anim;
    private Rigidbody2D rb;
    

    public Vector2 jumpKickForce;
    public Vector2 forwardKickForce;

    public Vector2 jumpKickKnockback;
    public Vector2 forwardKickKnockback;
    public Vector2 kickKnockback;
    
	public GameObject blast;
	public Transform blastPosition;

    private Hit hit;

    

    private bool facingRight;

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
        shotCounter -= Time.deltaTime;
        
        //---------------------------------------------------------------//
        // Light attacks

        // OneTwo Combo - L
        if (Input.GetKeyDown(KeyCode.O))
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            JumpKick();
        }

        // Two Side Attack - S K
        if(Input.GetKeyDown(KeyCode.K))
        {

            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                StartCoroutine(KiBlast());
            }
        }

        // Spinning Kick - D K
        if (Input.GetKeyDown(KeyCode.L))
        {
            ForwardKick();
        }

 

    }


    public void KickCombo()
    {
        KickComboFirstHit();

        if (Time.time - lastClicked < 0.5)
        {
            KickComboSecondHit();

        }
        else
        {
            KickComboThirdHit();
        }
        
        lastClicked = Time.time;
    }

    public void KickComboFirstHit()
    {
        anim.SetTrigger("KickCombo(0)");
        hit.SetDamage(4);
        hit.SetKnockback(kickKnockback);
        
    }

    public void KickComboSecondHit()
    {
        anim.SetTrigger("KickCombo(1)");
        hit.SetDamage(4);
        hit.SetKnockback(kickKnockback);
        
        
        
    }

	public void KickComboThirdHit()
    {
        anim.SetTrigger("KickCombo(2)");
        hit.SetDamage(4);
        hit.SetKnockback(kickKnockback);
        
        
        
    }

    public void JumpKick()
    {
        if(canJumpKick)
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

            canJumpKick = false;
            
        }

        hit.SetDamage(10);
        hit.SetKnockback(jumpKickKnockback);        
    }
    

    public void KiBlastButton()
    {
        if(shotCounter <= 0)
        {
            shotCounter = timeBetweenShots;
            StartCoroutine(KiBlast());
        }
    }

    IEnumerator KiBlast()
    {
        anim.SetTrigger("KiBlast");

        yield return new WaitForSeconds(0.4f);
        Instantiate(blast,blastPosition.position,Quaternion.identity);
        StopCoroutine(KiBlast());

    }

    public void ForwardKick()
    {
        if(canKick)
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
            
            canKick = false;
        }
        hit.SetDamage(5);
        hit.SetKnockback(forwardKickKnockback);
        

    }

    public void ForwardKickCheck()
    {
        canKick = true;
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TAG_FLOOR)
        {
            canJumpKick = true;
        }
    }

}
