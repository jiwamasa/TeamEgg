using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages title controls
public class TitleController : MonoBehaviour
{
    public string introCutscene; // Starting cutscene

    // Start game
    public void StartGame()
    {
        SceneManager.LoadScene(introCutscene);
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
} 

