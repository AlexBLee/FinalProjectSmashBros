using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private bool player1 = false;
    private bool player2 = false;

    private float h = 0.0f;    

    [HideInInspector]public bool facingRight = true;

    public float jumpVelocity = 15.0f;
    public bool canJump = true;

    private string TAG_FLOOR = "Floor";

    public float moveForce = 365f;
    public float maxSpeed = 5f;

    //private Joystick joystick;

    private Animator anim;
    private Rigidbody2D rb;


    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //joystick = FindObjectOfType<FixedJoystick>();

        if(gameObject.name == "DogFighter")
        {
            player1 = true;
        }
        else if(gameObject.name == "CatFighter")
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
            h = Input.GetAxis("Horizontal");
        
        if(player2)
            h = Input.GetAxis("Horizontal1");

        if (h * rb.velocity.x < maxSpeed)
        {
            anim.SetBool("Walking", true);
            
            rb.AddForce(Vector2.right * h * moveForce);
        }

        if(canJump && Input.GetKey(KeyCode.W)) //&& v > 0.8)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            canJump = false;
        }
        

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