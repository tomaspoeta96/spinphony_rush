using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    EventSystem eventSystem;
    public Button FightButton, OptionsButton, PhoniesButton, ExitGameButton;

    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        FightButton.onClick.AddListener(OnClickFight);
        OptionsButton.onClick.AddListener(OnClickOptions);
        PhoniesButton.onClick.AddListener(OnClickPhonies);
        ExitGameButton.onClick.AddListener(OnClickExit);
    }

    private void Update()
    {
        if (Input.anyKey && eventSystem.currentSelectedGameObject == null) FightButton.Select();
    }

    void OnClickFight()
    {
        SceneManager.LoadScene("Phony Selection");
    }

    void OnClickOptions()
    {
        SceneManager.LoadScene("Options");
    }

    void OnClickPhonies()
    {
        SceneManager.LoadScene("Phonies");
    }

    void OnClickExit()
    {
        Application.Quit();
    }

}
