using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DogControls : NetworkBehaviour
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
    private PlayerMovement playerMovement;
    private bool facingRight;
    
    // For the blast
	public GameObject blast;
	public Transform blastPosition;
    
    // Audio
    private AudioSource source;
    public AudioClip[] clips;

    // Mobile buttons
    MobileButtons mobileButtons;

    // ----------------------------------------------------------------------------------------------------

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
        // So you can't spam blast.
        shotCounter -= Time.deltaTime;

        if(!isLocalPlayer)
        {
            return;
        }


        // Two Kick Combo
        if (Input.GetKeyDown(KeyCode.U) || (mobileButtons != null && mobileButtons.A))
        {

            thirdKick = false;
            if(!isServer)
                CmdKickComboFirst();
            else
                RpcKickCombo();
            
            if (Time.time - lastClicked < 0.5 && thirdKick == false)
            {
                if(!isServer)
                    CmdKickComboSecond();
                else
                    RpcKickComboSecond();
                thirdKick = true;              
            }

            if(Time.time - lastClicked < 0.5 && thirdKick == true)
            {
                if(!isServer)
                    CmdKickComboThird();
                else
                    RpcKickComboThird(); 

                thirdKick = false;

            }

            lastClicked = Time.time;

            if(mobileButtons != null)
            {
                mobileButtons.A = false;
            }
        }

        // Flying Kick
        if (Input.GetKeyDown(KeyCode.I) || (mobileButtons != null && mobileButtons.B))
        {

            if(!isServer)
                CmdJumpKick();
            else
                RpcJumpKick();

            if(mobileButtons != null)
            {
                mobileButtons.B = false;
            }
        }

        // Two Side Attack
        if(Input.GetKeyDown(KeyCode.O) || (mobileButtons != null && mobileButtons.C))
        {
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                if(!isServer)
                    CmdKiBlast();
                else
                    RpcKiBlast();
            }

            if(mobileButtons != null)
            {
                mobileButtons.C = false;
            }
        }

        // Forward Kick
        if (Input.GetKeyDown(KeyCode.P) || (mobileButtons != null && mobileButtons.D))
        {
            if(!isServer)
                CmdForwardKick();
            else
                RpcForwardKick();
            
            if(mobileButtons != null)
            {
                mobileButtons.D = false;
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
        source.PlayOneShot(clips[0]);
    }

    public void KickComboSecondHit()
    {
        anim.SetTrigger("KickCombo(1)");
        hit.SetDamage(4);
        hit.SetKnockback(kickKnockback);
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
            playerMovement.enabled = false;            
            canHit = false;
            canJumpKick = false;
            source.PlayOneShot(clips[1]);

            
        }
        hit.SetDamage(10);
        hit.SetKnockback(jumpKickKnockback);       
         
    }

    // --------------------------------------------------------------------------------------------------------- //
    
    IEnumerator KiBlast()
    {
        if(canHit)
        {
            playerMovement.enabled = false;
            anim.SetTrigger("KiBlast");
        
            yield return new WaitForSeconds(0.4f);
            GameObject ball = Instantiate(blast,blastPosition.position,Quaternion.identity);
            ball.GetComponent<BlastController>().player = gameObject;
            StopCoroutine(KiBlast());
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
            source.PlayOneShot(clips[0]);

        }
        hit.SetDamage(5);
        hit.SetKnockback(forwardKickKnockback);
        

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

    // -----------------------------------------------------------------------------------
    // For networking.

    [Command]
    void CmdKiBlast()
    {
        RpcKiBlast();
    }

    [ClientRpc]
    void RpcKiBlast()
    {
        StartCoroutine(KiBlast());
    }

    // -----------------------------------------------------------------------------------


    [Command]
    void CmdForwardKick()
    {
        RpcForwardKick();
    }

    [ClientRpc]
    void RpcForwardKick()
    {
        ForwardKick();
    }
    // -----------------------------------------------------------------------------------


    [Command]
    void CmdJumpKick()
    {
        RpcJumpKick();
    }

    [ClientRpc]
    void RpcJumpKick()
    {
        JumpKick();
    }
    // -----------------------------------------------------------------------------------


    [Command]
    void CmdKickComboFirst()
    {
        RpcKickCombo();
    }

    [ClientRpc]
    void RpcKickCombo()
    {
        KickComboFirstHit();
    }

    // -----------------------------------------------------------------------------------


    [Command]
    void CmdKickComboSecond()
    {
        RpcKickComboSecond();
    }

    [ClientRpc]
    void RpcKickComboSecond()
    {
        KickComboSecondHit();
    }

    // -----------------------------------------------------------------------------------


    [Command]
    void CmdKickComboThird()
    {
        RpcKickComboThird();
    }

    [ClientRpc]
    void RpcKickComboThird()
    {
        KickComboThirdHit();
    }
}

