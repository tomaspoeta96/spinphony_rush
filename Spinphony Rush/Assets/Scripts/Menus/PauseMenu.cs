using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button OptionsButton, ResumeButton, MainMenuButton, PhonySelectButton;
    public GameObject pauseMenu;
    public Animator[] animators;
    public AudioSource[] audios;
    
    public List<float> animatorsSpeed = null;

    // Start is called before the first frame update
    void Start()
    {
        OptionsButton.onClick.AddListener(OnClickOptions);
        ResumeButton.onClick.AddListener(OnClickResume);
        MainMenuButton.onClick.AddListener(OnClickMainMenu);
        PhonySelectButton.onClick.AddListener(OnClickPhonySelect);
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

    private void OnClickPhonySelect()
    {
        SceneManager.LoadScene("Phony Selection");
    }

    private void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.Find("PhonySelectionData"));
        Destroy(GameObject.Find("OptionsData"));
    }

    private void OnClickOptions()
    {
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);
    }

    private void OnClickResume()
    {
        ContinueGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        animators = FindObjectsOfType(typeof(Animator)) as Animator[];
        audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (Animator animatorItem in animators)
        {
            animatorsSpeed.Add(animatorItem.speed);
            animatorItem.speed = 0f;

        }
        foreach (AudioSource audio in audios)
        {
            audio.Pause();

        }
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        animators = FindObjectsOfType(typeof(Animator)) as Animator[];
        audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (Animator animatorItem in animators)
        {
            foreach (float speed in animatorsSpeed)
            {
                animatorItem.speed = speed;
            }
        }

        foreach (AudioSource audio in audios)
        {
            audio.UnPause();
        }
    }

}
