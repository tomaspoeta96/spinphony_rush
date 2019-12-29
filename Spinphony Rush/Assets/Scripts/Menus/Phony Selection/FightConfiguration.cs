using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightConfiguration : MonoBehaviour
{
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(OnClickBack);
    }

    private void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }


}
