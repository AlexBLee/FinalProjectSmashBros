using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : NetworkBehaviour
{
    // Tag check
    private string TAG_FLOOR = "Floor";

    // To determine players.
    public List<GameObject> managerObjects;
    public bool player1 = false;
    public bool player2 = false;

    // Jump check
    public bool canJump = true;

    // Movement speeds/forces.
    public float jumpVelocity = 15.0f;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    private float h = 0.0f;
    private float v = 0.0f;

    // FOR MOBILE!
    public Joystick joystick;

    // Animation
    private Animator anim;

    // For moving.
    private Rigidbody2D rb;

    [SyncVar(hook = "FacingCallback")]
    [HideInInspector]public bool facingRight = true;

    // --------------------------------------------------------------------------------------------------------- //    
    void Start()
    {
        managerObjects = GameManager.instance.players;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Find joystick if on mobile.
        #if UNITY_ANDROID
            joystick = FindObjectOfType<FixedJoystick>();
        #endif
        
        
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        // If the player can jump and the player jumped, play the jump animation.
        if (!canJump)
            anim.SetBool("Jumping", true);
        else
            anim.SetBool("Jumping", false);
        

    }

    void FixedUpdate()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        
        h = Input.GetAxis("Horizontal");

        // For mobile implementation.
        #if UNITY_ANDROID

        h = joystick.Horizontal;
        v = joystick.Vertical;

        if(canJump && v > 0.8)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            canJump = false;
        }
        
        #endif

        
        // If the player has clicked jump, jump.
        if(canJump && Input.GetKeyDown(KeyCode.W)) //&& v > 0.8)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            canJump = false;
        }
            
        // Move in direction.
        if (h * rb.velocity.x < maxSpeed)
        {
            anim.SetBool("Walking", true);
            
            rb.AddForce(Vector2.right * h * moveForce);
        }

        
        // If the player has somehow moved faster than the max speed, then set their speed to max speed.
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            anim.SetBool("Walking", true);

        }

        // If the player isn't moving, then don't walk.
        // h is a number from 0-1 based on the player clicking movement buttons.
        if(h == 0)
        {
            anim.SetBool("Walking", false);
        }

        // If player has clicked right and is not already facing right, flip the character
        if (h > 0 && !facingRight || h < 0 && facingRight)
        {
            facingRight = !facingRight;
            CmdFlip(facingRight);
        }

        // If player has clicked left and is not already facing left, flip the character
        // else if (h < 0 && facingRight)
        //     CmdFlip();
        
    }

    // Flip the character.
    [Command]
    void CmdFlip(bool facing)
    {
        facingRight = facing;
        if(facingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 5;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -5;
            transform.localScale = theScale;
        }
    }

    // Flip the character.
    void FacingCallback(bool facing)
    {
        facingRight = facing;
        if(facingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 5;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -5;
            transform.localScale = theScale;
        }
    }

    // Hit the floor, can now jump.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TAG_FLOOR)
        {
            canJump = true;
        }
    }

}