using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFocus : MonoBehaviour 
{
	public float halfXBounds = 20f;
	public float halfYBounds = 15f;
	public float halfZBounds = 15f;

	public Bounds focusBounds;


	

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 position = gameObject.transform.position;

		Bounds bounds = new Bounds();

		bounds.Encapsulate(new Vector3(position.x - halfXBounds, position.y - halfYBounds, position.z - halfZBounds));
		bounds.Encapsulate(new Vector3(position.y + halfXBounds, position.y + halfYBounds, position.z + halfZBounds));
		
		focusBounds = bounds;
		
	}
}
