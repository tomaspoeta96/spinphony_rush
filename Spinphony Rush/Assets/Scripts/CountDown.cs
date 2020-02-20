using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour {
    public Text count;
    private double time;
    private int roundedTime;
    private Color trans = new Color();
    public Font font;

    void Start() {
        time = 30;
        trans[3] = 0.6f;
        trans[0] = 1;
    }

    void Update() {
        try {
                time -= Time.deltaTime;
                roundedTime = (int)time;

                if (roundedTime > 0) {
                    count.text = (roundedTime).ToString();
                    switch (roundedTime) {
                        case 10:
                            count.color = Color.yellow;
                            break;
                        case 5:
                            count.transform.position = new Vector3(950, 600, 0);
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
                }
                else if (roundedTime > -3) {
                    printTheEnd(count);
                }
                else {
                    GameRules.endGame = true;
                    SceneManager.LoadScene("Map Selection");
                }
            } catch (Exception) {
                print("Time must be a number");
            }
        }

    public void printTheEnd(Text txt) {
        txt.font = font;
        txt.text = "The end";
        txt.color = Color.red;
        txt.fontSize = 260;
    }
}
