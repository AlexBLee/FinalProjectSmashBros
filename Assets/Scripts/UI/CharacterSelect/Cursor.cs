using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour 
{
	// Colliders
	BoxCollider2D box;
	public Collider2D[] overlap;

	private void Start() 
	{
		box = GetComponent<BoxCollider2D>();
	}

	private void Update() 
	{
		// To find out what object is under the cursor.
		// In the character select, this is used to find out which character you want to play
		overlap = Physics2D.OverlapAreaAll(box.bounds.min,box.bounds.max);

		// If the cursor is over the other cursor, ignore it.
		// Note: The reason the array is checking for the 2nd element is because the cursor overlaps itself for the 1st element.
		if(overlap.Length > 1 && (overlap[1].name == "P2Cursor" || overlap[1].name == "P1Cursor"))
		{
			overlap[1] = null;
		}

	}

	// Drag the cursor with the mouse.
	private void OnMouseDrag() 
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		temp.z = 0;
		transform.position = temp;
	}

}
