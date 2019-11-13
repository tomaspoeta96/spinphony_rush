using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoost : MonoBehaviour {
    public GameObject[] boosts = new GameObject[16];
    public PhonyPlayerController phony;
    private int index = 0;
    public float elapsedTime = 0f;
    public float repeatTime = 5f;
    public bool on_map_Jump = false;
    public bool on_map_Shield = false;
    public bool on_map_Fuelle = false;
    public bool on_map_Move = false;

    void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= repeatTime) {
            index = Random.Range (0, boosts.Length);
            switch (boosts[index].name) {
                case "JumpBoost":
                    if(!on_map_Jump) {
                        boosts[index].SetActive(true);
                        on_map_Jump = true;
                    } 
                    break;
                case "ShieldBoost":
                    if(!on_map_Shield) {
                        boosts[index].SetActive(true);
                        on_map_Shield = true;
                    } 
                    break;
                case "FuelleBoost":
                    if(!on_map_Fuelle) {
                        boosts[index].SetActive(true);
                        on_map_Fuelle = true;
                    } 
                    break;
                case "MoveBoost":
                    if(!on_map_Move) {
                        boosts[index].SetActive(true);
                        on_map_Move = true;
                    } 
                    break;
            }
            elapsedTime -= repeatTime;
        }
    }
    
}
