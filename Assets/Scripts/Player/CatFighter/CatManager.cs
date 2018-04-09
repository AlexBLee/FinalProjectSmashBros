﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour 
{
	private Vector2 spawnPosition = new Vector2(0,0);
	private string TAG_KILLZONE = "KillZone";
    public Transform catPosition;

    void Start()
    {
        catPosition = GetComponent<Transform>();
    }
    

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TAG_KILLZONE)
        {
            transform.position = spawnPosition;
        }
    }
}