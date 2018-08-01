using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayScreen : MonoBehaviour
{
    // Buttons
    public Button soloModeButton;
    public Button multiplayerButton;

    // Tags
    private const string CHARACTER_SELECT = "SCharacterSelect";
    private const string MULTIPLAYER = "CharacterSelect";
    
    // --------------------------------------------------------------------------------------------------------- //        

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

    // Goes to character select.
    private void ToCharacterSelect()
    {
        SceneManager.LoadScene(CHARACTER_SELECT);
    }

    // Goes to a server browser of some sort? (Not implemented).
    private void ToMultiplayer()
    {
        SceneManager.LoadScene(MULTIPLAYER);
    }



}