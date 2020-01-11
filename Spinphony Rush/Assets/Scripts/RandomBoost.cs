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
            GameObject[] phonies = GameObject.FindGameObjectsWithTag("Phony");
            float distance = Mathf.Infinity;
            GameObject closest = null;
            foreach (GameObject go in phonies)
            {
                Vector3 diff = go.transform.position - boosts[index].transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            switch (boosts[index].name) {
                case "JumpBoost":
                    if(!on_map_Jump) {
                        if (Vector3.Distance(boosts[index].transform.position, closest.transform.position) > 3)
                        {
                            boosts[index].SetActive(true);
                        }
                        on_map_Jump = true;
                        boosts[index].GetComponentInChildren<Animator>().SetBool("DisplaySalto", true);
                    } 
                    break;
                case "ShieldBoost":
                    if(!on_map_Shield) {
                        boosts[index].SetActive(true);
                        on_map_Shield = true;
                        boosts[index].GetComponentInChildren<Animator>().SetBool("DisplayShield", true);
                    } 
                    break;
                case "FuelleBoost":
                    if(!on_map_Fuelle) {
                        boosts[index].SetActive(true);
                        on_map_Fuelle = true;
                        boosts[index].GetComponentInChildren<Animator>().SetBool("DisplayFuelle", true);
                    } 
                    break;
                case "MoveBoost":
                    if(!on_map_Move) {
                        boosts[index].SetActive(true);
                        on_map_Move = true;
                        boosts[index].GetComponentInChildren<Animator>().SetBool("DisplayHandle", true);
                    } 
                    break;
            }
            elapsedTime -= repeatTime;
        }
    }
    
}
