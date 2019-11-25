using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button OptionsButton, ResumeButton, MainMenuButton;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        OptionsButton.onClick.AddListener(OnClickOptions);
        ResumeButton.onClick.AddListener(OnClickResume);
        MainMenuButton.onClick.AddListener(OnClickMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseMenu.active == false) PauseGame();
            else ContinueGame();
        }
    }

    private void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnClickOptions()
    {
        SceneManager.LoadScene("Options");
    }

    private void OnClickResume()
    {
        ContinueGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        //enable the scripts again
    }

}
