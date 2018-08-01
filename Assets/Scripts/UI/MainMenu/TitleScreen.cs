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
    }

}