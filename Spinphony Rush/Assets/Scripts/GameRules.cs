using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    //private PhonySelector selector = PhonySelector.Instance;
    public int players;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject hud1;
    public GameObject hud2;
    public GameObject hud3;
    public GameObject hud4;

    private GameObject[] playerss;
    private GameObject[] HUD;
    private bool hasSum = false;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //players = selector.phony_count;
        //players=1;,
        //playerss = GameObject.FindGameObjectsWithTag("Player");
        //HUD = GameObject.FindGameObjectsWithTag("HUD");
        //print(playerss.Length);

        player1.SetActive(true);
        hud1.SetActive(true);
        if(players >= 2) {
            player2.SetActive(true);
            hud2.SetActive(true);
        }
        if(players >= 3) {
            player3.SetActive(true);
            hud3.SetActive(true);
        }
        if(players == 4) {
            player4.SetActive(true);
            hud4.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
 
    }

}
