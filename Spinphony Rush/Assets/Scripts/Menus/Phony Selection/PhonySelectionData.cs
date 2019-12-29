using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhonySelectionData : MonoBehaviour
{
    public String Player1Controller, Player2Controller, Player3Controller, Player4Controller;
    public int numPlayers;
    private StartFightButton startFightScript;

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
            numPlayers = startFightScript.numPlayers();
            if(GameObject.Find("Label1") != null) {
                Player1Controller = GameObject.Find("Label1").GetComponent<Text>().text;
            }
            else { Player1Controller = null; }
            if (GameObject.Find("Label2") != null) {
                Player2Controller = GameObject.Find("Label2").GetComponent<Text>().text;
            }
            else { Player2Controller = null; }
            if (GameObject.Find("Label3") != null) {
                Player3Controller = GameObject.Find("Label3").GetComponent<Text>().text;
            }
            else { Player3Controller = null; }
            if (GameObject.Find("Label4") != null) {
                Player4Controller = GameObject.Find("Label4").GetComponent<Text>().text;
            }
            else { Player4Controller = null; }
        }
    }

}
