using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Text title;
    public Text enterText;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            MainMenuUIManager.instance.ToMainMenuScreen();
        }
    }

}