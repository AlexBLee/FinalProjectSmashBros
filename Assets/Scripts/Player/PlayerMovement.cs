﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector]public bool facingRight = true;

    public float jumpVelocity = 500.0f;
    private bool canJump = true;

    private string TAG_FLOOR = "Floor";

    public float moveForce = 365f;
    public float maxSpeed = 5f;


    private Animator anim;
    private Rigidbody2D rb;


    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        float h = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W) && canJump)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            canJump = false;
        }

        if (h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * moveForce);
            anim.SetBool("Walking", true);
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