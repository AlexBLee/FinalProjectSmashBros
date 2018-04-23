using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
	private Vector2 spawnPosition = new Vector2(0,0);
	private string TAG_KILLZONE = "KillZone";
    private int lives = 5;
    private Rigidbody2D rb;
    [HideInInspector] public Transform dogPosition;
    private PlayerHealth playerHealth;

    void Start()
    {
        dogPosition = GetComponent<Transform>();
        playerHealth = GetComponent<PlayerHealth>();   
        rb = GetComponent<Rigidbody2D>();     
        
    }

    void Update()
    {
        if(lives <= 0)
        {
            Destroy(gameObject);
        }
    }

	private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TAG_KILLZONE)
        {
            playerHealth.health = 0;
            --lives;
            yield return new WaitForSeconds(2);
            rb.velocity = new Vector2(0,0);
            transform.position = spawnPosition;
            Debug.Log(lives);
        }
    }


}
