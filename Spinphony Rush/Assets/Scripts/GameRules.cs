using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    //private PhonySelector selector = PhonySelector.Instance;
    private int players;
    public DataObject Data;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject hud1;
    public GameObject hud2;
    public GameObject hud3;
    public GameObject hud4;

    // Start is called before the first frame update
    void Start()
    {
        players = DataObject.numJugadores;

        player1.SetActive(true);
        hud1.SetActive(true);
        //player1.GetComponentInChildren<PhonyPlayerController>().setKeys(DataObject.player1Keyboard);
        /*player1.GetComponentInChildren<PhonyPlayerController>().right = (KeyCode)System.Enum.Parse(typeof(KeyCode), DataObject.player1Keyboard.RIGHT()) ;
        player1.GetComponentInChildren<PhonyPlayerController>().left = (KeyCode)System.Enum.Parse(typeof(KeyCode), DataObject.player1Keyboard.LEFT()) ;
        player1.GetComponentInChildren<PhonyPlayerController>().up = (KeyCode)System.Enum.Parse(typeof(KeyCode), DataObject.player1Keyboard.UP()) ;
        player1.GetComponentInChildren<PhonyPlayerController>().down = (KeyCode)System.Enum.Parse(typeof(KeyCode), DataObject.player1Keyboard.DOWN()) ;
        player1.GetComponentInChildren<PhonyPlayerController>().hability = (KeyCode)System.Enum.Parse(typeof(KeyCode), DataObject.player1Keyboard.HABILITY()) ;
        player1.GetComponentInChildren<PhonyPlayerController>().boost = (KeyCode)System.Enum.Parse(typeof(KeyCode), DataObject.player1Keyboard.BOOST()) ;
        player1.GetComponentInChildren<PhonyPlayerController>().reverb = (KeyCode)System.Enum.Parse(typeof(KeyCode), DataObject.player1Keyboard.REVERB()) ;
        */

        if(players >= 2) {
            player2.SetActive(true);
            hud2.SetActive(true);
            //player2.GetComponentInChildren<PhonyPlayerController>().setKeys(DataObject.player2Keyboard);
        }
        if(players >= 3) {
            player3.SetActive(true);
            hud3.SetActive(true);
            //player3.GetComponentInChildren<PhonyPlayerController>().setKeys(DataObject.player3Keyboard);
        }
        if(players == 4) {
            player4.SetActive(true);
            hud4.SetActive(true);
            //player4.GetComponentInChildren<PhonyPlayerController>().setKeys(DataObject.player4Keyboard);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


}
