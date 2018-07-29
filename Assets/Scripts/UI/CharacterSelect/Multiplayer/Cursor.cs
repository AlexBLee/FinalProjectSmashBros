using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Cursor : NetworkBehaviour
{
	// Colliders
	BoxCollider2D box;
	public Collider2D[] overlap;

	private void Start() 
	{
		box = GetComponent<BoxCollider2D>();

		// Assign cursors to images
		if(gameObject.name == "P1Cursor(Clone)")
		{
			GameObject.Find("P1").GetComponent<CharacterSelect>().cursor = this;
		}

		if(gameObject.name == "P2Cursor(Clone)")
		{
			
			GameObject.Find("P2").GetComponent<CharacterSelect>().cursor = this;
		}

		Mathf.Abs(Camera.main.orthographicSize - (GetComponent<SpriteRenderer>().sprite.texture.height / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit));
		
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
		if(!isLocalPlayer)
		{
			return;
		}
		
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		temp.z = 0;
		transform.position = temp;
	}

}
