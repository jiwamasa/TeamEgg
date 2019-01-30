using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages title controls
public class TitleController : MonoBehaviour
{
    public string introCutscene; // Starting cutscene

    public Animator introAnimation;

    // Start game
    public void StartGame()
    {
        introAnimation.SetTrigger("Start");
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
} 

