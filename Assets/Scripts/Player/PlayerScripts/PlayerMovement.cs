using UnityEngine;
using System.Collections.Generic;

using System.Collections;

public class PlayerMovement : MonoBehaviour
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

    // FOR MOBILE!
    //private Joystick joystick;

    // Animation
    private Animator anim;

    // For moving.
    private Rigidbody2D rb;
    [HideInInspector]public bool facingRight = true;

    // --------------------------------------------------------------------------------------------------------- //    


    void Start()
    {
        managerObjects = GameManager.instance.players;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //joystick = FindObjectOfType<FixedJoystick>();

        // Find out the player number.
        if(managerObjects[0].name == gameObject.name)
        {
            player1 = true;
        }
        else if(managerObjects[1].name == gameObject.name)
        {
            player2 = true;
        }
        
    }

    void Update()
    {
        // If the player can jump and the player jumped, play the jump animation.
        if (!canJump)
            anim.SetBool("Jumping", true);
        else
            anim.SetBool("Jumping", false);
    }

    void FixedUpdate()
    {
        //float h = joystick.Horizontal;
        //float v = joystick.Vertical;

        // Player 1 controls.
        if(player1)
        {
            h = Input.GetAxis("Horizontal");

            if(canJump && Input.GetKeyDown(KeyCode.W)) //&& v > 0.8)
            {
                rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                canJump = false;
            }
        
        }
        
        // Player 2 Controls.
        if(player2)
        {
            h = Input.GetAxis("Horizontal1");

            if(canJump && Input.GetKeyDown(KeyCode.UpArrow)) //&& v > 0.8)
            {
                rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                canJump = false;
            }
            
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
        if (h > 0 && !facingRight)
            Flip();

        // If player has clicked left and is not already facing left, flip the character
        else if (h < 0 && facingRight)
            Flip();
        
    }

    // Flip the character.
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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