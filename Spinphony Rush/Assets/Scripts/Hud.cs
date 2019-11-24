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
        if(phony.haveJump || phony.haveShield || phony.haveFuelle || phony.haveMove)
            boost.enabled = true;
        if(!phony.haveJump && !phony.haveShield && !phony.haveFuelle && !phony.haveMove)
            boost.enabled = false;
    }

    void incresePoints() {
        if (survive >= repeatTime) {
            survive = 0f;
            if(!phony.muerto) phony.points += 50;
        }
        text_points.text = (phony.points).ToString();
    }

    void viewBoost() {
        if(phony.haveJump)
            boost.material = jump;
        if(phony.haveShield)
            boost.material = shield;
        if(phony.haveFuelle)
            boost.material = fuelle;
        if(phony.haveMove)
            boost.material = move;
    }
        
}
