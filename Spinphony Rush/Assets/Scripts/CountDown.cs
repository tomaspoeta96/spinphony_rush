using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public Text count;
    private double time;
    private Color trans = new Color();
    public Font font;
    // Start is called before the first frame update
    void Start()
    {
        //count = gameObject.GetComponent<Text>();
        time = 31;
        trans[3] = 0.6f; trans[0] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        
        if((int)time > 0) {
            count.text = ((int)time).ToString();
            switch ((int)time) {
                case 10:
                    count.color = Color.yellow;
                    break;
                case 5:
                    count.transform.position = new Vector3(950,600,0);
                    count.fontSize = 130;
                    count.color = trans;
                    break;
                case 3:
                    count.fontSize = 140;
                    break;
                case 2:
                    count.fontSize = 150;
                    break;
                case 1:
                    count.fontSize = 160;
                    break;
            } 
        } else if((int)time > -3){
            the_end(count);
        } else {
            SceneManager.LoadScene("Map Selection");
        }
    }

    public void the_end(Text c) {
            c.font = font;
            c.text = "The end";
            c.color = Color.red;
            c.fontSize = 260;
    }
}
