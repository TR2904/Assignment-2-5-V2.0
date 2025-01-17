using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour

{

    public GameObject instructionsPanel;


    // Play Button: Load Game Scene

    public void PlayGame()

    {

        SceneManager.LoadScene("SampleScene"); // Replace "GameScene" with your actual scene name

    }


    // Instructions Button: Show Instructions Panel

    public void ShowInstructions()

    {

        instructionsPanel.SetActive(true);

    }


    // Back Button: Hide Instructions Panel

    public void HideInstructions()

    {

        instructionsPanel.SetActive(false);

    }


    // Quit Button: Exit Application

    public void QuitGame()

    {

        Application.Quit();

        Debug.Log("Game Quit!"); // Works only in built version

    }

}
