using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private Image boost;
    private Text name;
    private Text text_points;
    public PhonyPlayerController phony;
    private Material jump;
    private Material shield;
    private Material fuelle;
    private Material move;

    private const String pathToMaterial = "Materials/";

    private float survive = 0f;
    private float repeatTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        boost = this.gameObject.transform.Find("Boost").GetComponent<Image>();
        name = this.gameObject.transform.Find("Name").GetComponent<Text>();
        text_points = this.gameObject.transform.Find("Points").GetComponent<Text>();
        jump =      Resources.Load<Material>(pathToMaterial + "Salto");
        shield =    Resources.Load<Material>(pathToMaterial + "Escudo");
        fuelle =    Resources.Load<Material>(pathToMaterial + "Fuelle");
        move =      Resources.Load<Material>(pathToMaterial + "Movimiento");
        boost.enabled = false;
        text_points.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        survive += Time.deltaTime;
        controllerBoost();
        viewBoost();
        incresePoints();
    }

    void controllerBoost() {
        if(phony.getHaveJump() || phony.getHaveShield() || phony.getHaveFuelle() || phony.getHaveMove())
            boost.enabled = true;
        if(!phony.getHaveJump() && !phony.getHaveShield() && !phony.getHaveFuelle() && !phony.getHaveMove())
            boost.enabled = false;
    }

    void incresePoints() {
        if (survive >= repeatTime) {
            survive = 0f;
            if(!phony.getMuerto()) phony.setPoints(phony.getPoints() + 50);
        }
        text_points.text = (phony.getPoints()).ToString();
    }

    void viewBoost() {
        if(phony.getHaveJump())
            boost.material = jump;
        if(phony.getHaveShield())
            boost.material = shield;
        if(phony.getHaveFuelle())
            boost.material = fuelle;
        if(phony.getHaveMove())
            boost.material = move;
    }
        
}
