using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour
{
    public Text count;
    private double time;
    // Start is called before the first frame update
    void Start()
    {
        //count = gameObject.GetComponent<Text>();
        time = 31;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        count.text = ((int)time).ToString();
        switch ((int)time)
        {
            case 27:
                count.color = Color.red;
                break;
            case 25:
                count.transform.Translate(0, -6f, 0);
                break;
            case 0:
                count.text = 0.ToString();
                break;
        }
    }
}
