using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour
{
    public Text count;
    private double time;
    private Color trans = new Color();
    // Start is called before the first frame update
    void Start()
    {
        //count = gameObject.GetComponent<Text>();
        time = 31;
        trans[3] = 0.5f; trans[0] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        count.text = ((int)time).ToString();
        
        if((int)time > 0) 
            switch ((int)time) {
                case 10:
                    count.color = Color.red;
                    break;
                case 5:
                    count.transform.Translate(0, -6f, 0);
                    count.fontSize = 60;
                    count.color = trans;
                    break;
                case 3:
                    count.fontSize = 80;
                    break;
                case 2:
                    count.fontSize = 90;
                    break;
                case 1:
                    count.fontSize = 100;
                    break;
        } 
        else {
            count.text = "Game Over";
            count.color = Color.red;
        }
    }
}
