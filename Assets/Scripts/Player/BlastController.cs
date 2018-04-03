using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    public float speed = 0.5f;
    private PlayerMovement playerMovement;
    private Rigidbody2D rBody;
    

	// Use this for initialization
	void Start ()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        rBody = GetComponent<Rigidbody2D>();
        shootBullet();

        
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void shootBullet()
    {
        if (playerMovement.facingRight != true)
				rBody.velocity += (new Vector2(-100,0) * Time.deltaTime * speed);
			else
				rBody.velocity += (new Vector2(100,0) * Time.deltaTime * speed);
    }
}
