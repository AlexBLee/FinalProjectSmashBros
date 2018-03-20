using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpVelocity = 500.0f;
    private bool canJump = true;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && canJump)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            canJump = false;
        }

        //if (!canJump)
        //{
        //    animator.SetBool("Jump", true);
        //}
        //else
        //{
        //    animator.SetBool("Jump", false);

        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }
}