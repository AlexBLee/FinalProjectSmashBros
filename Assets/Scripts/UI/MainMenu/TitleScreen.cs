using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------- //    

    // Go to menu from title screen.
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            MainMenuUIManager.instance.ToMainMenuScreen();
        }

        #if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                MainMenuUIManager.instance.ToMainMenuScreen();            
            }
        #endif
    }

}