using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonySelector : MonoBehaviour
{
    public Button addButton;
    public GameObject tarjeta;
    public Button readyButton;

    public int phony_count = 1;

    public static PhonySelector Instance;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(showTarjeta);
        readyButton.onClick.AddListener(ready);
    }

    private void showTarjeta()
    {
        addButton.gameObject.SetActive(false);
        tarjeta.SetActive(true);
        phony_count++;
    }

    private void ready()
    {
        if (readyButton.interactable == true) readyButton.interactable = false;
    }

    public int getPhonyCount() {
        return phony_count;
    }

}
