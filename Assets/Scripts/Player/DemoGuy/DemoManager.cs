using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour 
{
	private Vector2 spawnPosition = new Vector2(0,0);
	private string TAG_KILLZONE = "KillZone";
    private CatHealth catHealth;

    void Start()
    {
        catHealth = GetComponent<CatHealth>();
    }
    

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TAG_KILLZONE)
        {
            transform.position = spawnPosition;
            catHealth.health = 0f;
        }
    }
}
