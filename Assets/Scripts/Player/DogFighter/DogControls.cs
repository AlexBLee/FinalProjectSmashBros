using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogControls : MonoBehaviour
{
    bool canHit = true;
    bool canJumpKick = true;
    bool thirdKick = false;
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
    private Hit hit;
    
	public GameObject blast;
	public Transform blastPosition;

    private bool facingRight;

    private PlayerMovement playerMovement;


    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        hit = GetComponent<Hit>();
        
	}


    void Update ()
    {
        shotCounter -= Time.deltaTime;

        if(playerMovement.player1)
        {
             // Two Kick Combo
            if (Input.GetKeyDown(KeyCode.U))
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

            // Two Side Attack
            if(Input.GetKeyDown(KeyCode.O))
            {
                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    StartCoroutine(KiBlast());
                }
            }

            // Forward Kick
            if (Input.GetKeyDown(KeyCode.P))
            {
                ForwardKick();
            }

        }

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

            // Two Side Attack
            if(Input.GetKeyDown(KeyCode.Keypad2))
            {
                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    StartCoroutine(KiBlast());
                }
            }

            // Forward Kick
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                ForwardKick();
            }

        }

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
            playerMovement.enabled = false;            
            canHit = false;
            canJumpKick = false;
            
        }
        hit.SetDamage(10);
        hit.SetKnockback(jumpKickKnockback);       
         
    }
    

    IEnumerator KiBlast()
    {
        if(canHit)
        {
            playerMovement.enabled = false;
            anim.SetTrigger("KiBlast");
        
            yield return new WaitForSeconds(0.4f);
            Instantiate(blast,blastPosition.position,Quaternion.identity);
            StopCoroutine(KiBlast());
        }

    }

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
            playerMovement.enabled = false;
            canHit = false;
        }
        hit.SetDamage(5);
        hit.SetKnockback(forwardKickKnockback);
        

    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TAG_FLOOR)
        {
            canJumpKick = true;
        }
    }


    public void EnableDogControl()
    {
        //Debug.Log("!");
        playerMovement.enabled = true;
        canHit = true;
    }

}
