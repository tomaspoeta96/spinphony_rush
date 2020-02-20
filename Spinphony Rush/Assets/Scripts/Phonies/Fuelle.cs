using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuelle : MonoBehaviour {
    public Slider fuelleSlider;
    private Image fuelleFill;
    private Image fuelleIcon;
    private float duracion = 30f; //segundos
    public GameObject player;
    private bool shield;

    private Vector3 followOffset;

    void Start() {
        followOffset = transform.position - player.transform.position;
        fuelleSlider = GetComponentInChildren<Slider>();
        fuelleFill = transform.GetChild(0).GetChild(0).GetChild(2).transform.Find("Fill").GetComponent<Image>();
        fuelleFill = transform.Find("Canvas").Find("Slider").Find("Fill Area").Find("Fill").GetComponent<Image>();
        fuelleIcon = transform.Find("Canvas").Find("Slider").Find("Icon").GetComponent<Image>();
    }

    private void FixedUpdate() {
        FollowPlayer();
        changeColor();
        ConsumeOverTime();  
    }

    private void FollowPlayer() {
        try {
            Vector3 targetPosition = player.transform.position + followOffset;
            transform.position += (targetPosition - transform.position);
        }
        catch (MissingReferenceException e) {
            print("Player not assigned to " + this.name);
            print("Destroying " + this.name);
            Destroy(this);
        }
    }

    private void ConsumeOverTime() {
        float consumoPorSegundo = 1 / duracion;
            if(fuelleSlider.value > 0) fuelleSlider.value -= consumoPorSegundo * 0.02f;
    }

    private void changeColor() {
        Color color;
        if(fuelleSlider.value < 0.75) {
            color = new Color(0.8f, 0.8f, 0.1f, 1);
            if (fuelleSlider.value < 0.5) {
                color = new Color(0.8f, 0.6f, 0.2f, 1);
                if (fuelleSlider.value < 0.25) {
                    color = new Color(0.8f, 0.3f, 0.2f, 1);
                }
            }
        }
        else {
            color = new Color(0.1f, 0.6f, 0.2f, 1);
        }

        fuelleFill.color = color;
        fuelleIcon.color = color;
    }
    public bool getHasShield() {
        return shield;
    }

    public void setShield(bool shield) {
        this.shield = shield;
    }
}
