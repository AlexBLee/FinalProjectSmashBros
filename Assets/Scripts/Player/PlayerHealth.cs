using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private string TAG_CHARACTER = "Character";
    private Rigidbody2D rb;
    public float health = 0f;
    public Vector2 damage;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == TAG_CHARACTER)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                HitTakenRight();
                Debug.Log("hit right!");
            }

            if (collision.transform.position.x < transform.position.x)
            {
                HitTakenLeft();
                Debug.Log("hit left!");
            }
        }
        

        
    }

    void HitTakenRight()
    {
        health += 10.0f;
        Debug.Log("hit!");
        Debug.Log(health);

        damage = new Vector2(health * -10, health * -10);

        
        Debug.Log(damage);

        rb.AddForce(damage);
    }

    void HitTakenLeft()
    {
        health += 10.0f;
        Debug.Log("hit!");
        Debug.Log(health);

        damage = new Vector2(health * 10, health * 10);

        Debug.Log(damage);

        rb.AddForce(damage);
    }
}
