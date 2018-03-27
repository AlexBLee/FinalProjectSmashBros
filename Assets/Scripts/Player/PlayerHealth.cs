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
            HitTaken();
        }
    }

    void HitTaken()
    {
        health += 10.0f;
        Debug.Log("hit!");
        Debug.Log(health);

        damage = new Vector2(health * 10, health * 10);

        rb.AddForce(damage);
    }
}
