using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayScreen : MonoBehaviour
{
    public Button soloModeButton;
    public Button multiplayerButton;

    private const string CHARACTER_SELECT = "CharacterSelect";
    private const string MULTIPLAYER = "CharacterSelect";
    

    void Start ()
    {
        if (soloModeButton != null)
        {
            soloModeButton.onClick.AddListener(ToCharacterSelect);
        }

        if (multiplayerButton != null)
        {
            multiplayerButton.onClick.AddListener(ToMultiplayer);
        }
    }

    private void ToCharacterSelect()
    {
        SceneManager.LoadScene(CHARACTER_SELECT);
    }

    private void ToMultiplayer()
    {
        SceneManager.LoadScene(MULTIPLAYER);
    }



}