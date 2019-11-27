using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataObject : MonoBehaviour
{
    public String Player1Controller, Player2Controller, Player3Controller, Player4Controller;
    public static int numJugadores;
    private StartFightButton startFightScript;

    public static KeysTable player1Keyboard;
    public static KeysTable player2Keyboard;
    public static KeysTable player3Keyboard;
    public static KeysTable player4Keyboard;

    private List<string> players = new List<string>();
    private List<KeysTable> keystables = new List<KeysTable>();
    

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Phony Selection"))
        {
            startFightScript = GameObject.Find("Start Fight").GetComponent<StartFightButton>();
            numJugadores = startFightScript.numPlayers();
            if(GameObject.Find("Label1") != null) {
                Player1Controller = GameObject.Find("Label1").GetComponent<Text>().text;
                players.Add(Player1Controller);
                keystables.Add(player1Keyboard);
            }
            if (GameObject.Find("Label2") != null) {
                Player2Controller = GameObject.Find("Label2").GetComponent<Text>().text;
                players.Add(Player2Controller);
                keystables.Add(player2Keyboard);
            }
            if (GameObject.Find("Label3") != null) {
                Player3Controller = GameObject.Find("Label3").GetComponent<Text>().text;
                players.Add(Player3Controller);
                keystables.Add(player3Keyboard);
            }
            if (GameObject.Find("Label4") != null) {
                Player4Controller = GameObject.Find("Label4").GetComponent<Text>().text;
                players.Add(Player4Controller);
                keystables.Add(player4Keyboard);
            }
        }
    }

    void setControls() {
        for(int i = 0; i<players.Count; i++) {
            switch (players[i]) {
                case "Keyboard1":
                    keystables[i] = new KeysTable("D","W","A","S","SPACE","Q","E");
                    break;
                case "Keyboard2":
                    keystables[i] = new KeysTable("L","I","J","K","M","U","O");
                    break;
                case "GamePad1":
                    keystables[i] = new KeysTable("6","8","4","5","0","7","9");
                    break;
            }
        }
    }
}
