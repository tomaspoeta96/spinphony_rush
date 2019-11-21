using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonySelectioner : MonoBehaviour
{
    public Sprite[] peonzas;
    public GameObject peonza;
    public Button rightButton, leftButton;

    private int currentSprite;
    // Start is called before the first frame update
    void Start()
    {
        currentSprite = 0;
        peonza.GetComponent<Image>().sprite = peonzas[currentSprite];
        rightButton.onClick.AddListener(OnLeft);
        leftButton.onClick.AddListener(OnRight);
    }

    // Update is called once per frame
    void Update()
    {
        peonza.GetComponent<Image>().sprite = peonzas[currentSprite];
    }

    private void OnLeft()
    {
        if (peonza.GetComponent<Image>().sprite == peonzas[0])
            currentSprite = peonzas.Length - 1;
        else currentSprite--;
    }

    private void OnRight()
    {
        if (peonza.GetComponent<Image>().sprite == peonzas[peonzas.Length - 1])
            currentSprite = 0;
        else currentSprite++;
    }



}
