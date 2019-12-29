using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightConfiguration : MonoBehaviour
{
    public Button backButton, startButton;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(OnClickBack);
        startButton.onClick.AddListener(OnClickStart);
    }

    private void OnClickStart()
    {
        SceneManager.LoadScene("Map Selection");
    }

    private void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }


}
