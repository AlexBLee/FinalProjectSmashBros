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


    // Use this for initialization
    void Start()
    {
        managerObjects = GameManager.instance.players;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //joystick = FindObjectOfType<FixedJoystick>();


        if(managerObjects[0].name == gameObject.name)
        {
            player1 = true;
        }
        else if(managerObjects[1].name == gameObject.name)
        {
            player2 = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canJump)
            anim.SetBool("Jumping", true);
        else
            anim.SetBool("Jumping", false);
    }

    void FixedUpdate()
    {
        //float h = joystick.Horizontal;
        //float v = joystick.Vertical;

        if(player1)
        {
            h = Input.GetAxis("Horizontal");

            if(canJump && Input.GetKeyDown(KeyCode.W)) //&& v > 0.8)
            {
                rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                canJump = false;
            }
        
        }
            
        if(player2)
        {
            h = Input.GetAxis("Horizontal1");

            if(canJump && Input.GetKeyDown(KeyCode.UpArrow)) //&& v > 0.8)
            {
                rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                canJump = false;
            }
            
        }
            

        if (h * rb.velocity.x < maxSpeed)
        {
            anim.SetBool("Walking", true);
            
            rb.AddForce(Vector2.right * h * moveForce);
        }


        // if(canJump && Input.GetKeyDown(KeyCode.W)) //&& v > 0.8)
        // {
        //     rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        //     canJump = false;
        // }
        

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            anim.SetBool("Walking", true);

        }

        if(h == 0)
        {
            anim.SetBool("Walking", false);
        }

        if (h > 0 && !facingRight)
            Flip();

        else if (h < 0 && facingRight)
            Flip();
        
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TAG_FLOOR)
        {
            canJump = true;
        }
    }

}