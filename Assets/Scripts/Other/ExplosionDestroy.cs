using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour {

	public float time;

	// Destroy when done animation.
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
	
	
}
