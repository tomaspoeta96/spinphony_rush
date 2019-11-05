using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoost : MonoBehaviour
{
    public GameObject[] jump_boost = new GameObject[4];
    // pos 0 jump
    // pos 1 shield
    // pos 2 fuelle
    // pos 3 move
    //public GameObject[] shield_boost = new GameObject[2];
    private int j_index, s_index = 0;
    public float elapsedTime = 0f;
    public float repeatTime = 5f;


    // Update is called once per frame
    /*void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= repeatTime) {
            //if ()
            int new_j_index = Random.Range (0, jump_boost.Length);
            //int new_s_index = Random.Range (0, shield_boost.Length);
            jump_boost[j_index].SetActive(false);
            //shield_boost[s_index].SetActive(false);
            j_index = new_j_index;
            //s_index = new_s_index;
            jump_boost[j_index].SetActive(true);
            //shield_boost[s_index].SetActive(true);
            elapsedTime -= repeatTime;
        }
    }
    */

    private bool jump_is_free() {
        return true;
    }
    
}
