﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuelle : MonoBehaviour
{
    private Slider fuelleSlider;
    private Image fuelleFill;
    private float consumoPorSegundo = (float)0.01;
    private int UpdateCount = 0;
    public GameObject player;

    Vector3 _followOffset;

    void Start()
    {
        _followOffset = transform.position - player.transform.position;
        fuelleSlider = GetComponentInChildren<Slider>();
        fuelleFill = transform.GetChild(0).GetChild(0).GetChild(2).transform.Find("Fill").GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
        changeColor();
        ConsumeOverTime();

    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position + _followOffset;
        transform.position += (targetPosition - transform.position);
    }

    private void ConsumeOverTime()
    {
            if(fuelleSlider.value > 0) fuelleSlider.value -= consumoPorSegundo*(float)0.02;
    }

    private void changeColor()
    {
        Color color;
        if(fuelleSlider.value < 0.75){
            color = new Color((float)0.8, (float)0.8, (float)0.1, 1);
            if (fuelleSlider.value < 0.5)
            {
                color = new Color((float)0.8, (float)0.6, (float)0.2, 1);
                if (fuelleSlider.value < 0.25)
                {
                    color = new Color((float)0.8, (float)0.3, (float)0.2, 1);
                }
            }
        }
        else {
            color = new Color((float)0.1, (float)0.6, (float)0.2, 1);
        }

        fuelleFill.color = color;
    }
}
