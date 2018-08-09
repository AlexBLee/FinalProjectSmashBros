using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

	// Don't destroy self going through scenes.
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	
	
	
}
