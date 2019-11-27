using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Image boost;
    public Text name;
    public Text text_points;
    public PhonyPlayerController phony;
    public Material jump;
    public Material shield;
    public Material fuelle;
    public Material move;

    private float survive = 0f;
    private float repeatTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
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
