﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour 
{
	BoxCollider2D box;
	public Collider2D[] overlap;

	private void Start() 
	{
		box = GetComponent<BoxCollider2D>();
	}

	private void Update() 
	{
		overlap = Physics2D.OverlapAreaAll(box.bounds.min,box.bounds.max);
	}

	private void OnMouseDrag() 
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		temp.z = 0;
		transform.position = temp;
	}

}