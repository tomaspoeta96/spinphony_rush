using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    //private PhonySelector selector = PhonySelector.Instance;
    public PhonySelectionData PhonySelectionData;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject hud1;
    public GameObject hud2;
    public GameObject hud3;
    public GameObject hud4;

    public KeysTable Keyboard1 =
        new KeysTable(KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.Space, KeyCode.Q, KeyCode.E);
    public KeysTable Keyboard2 =
        new KeysTable(KeyCode.L, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.M, KeyCode.U, KeyCode.O);
    public KeysTable Keyboard3 =
        new KeysTable(KeyCode.Keypad6, KeyCode.Keypad8, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad0, KeyCode.Keypad7, KeyCode.Keypad9);
    public KeysTable GamePad1;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PhonySelectionData = GameObject.Find("PhonySelectionData").GetComponent<PhonySelectionData>();
        ConfigurePhonies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ConfigurePhonies()
    {
        if (PhonySelectionData.Player1Controller != null)
        {
            player1.SetActive(true);
            hud1.SetActive(true);
            player1.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player1Controller));
        }

        if (PhonySelectionData.Player2Controller != null)
        {
            player2.SetActive(true);
            hud2.SetActive(true);
            player2.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player2Controller));
        }

        if (PhonySelectionData.Player3Controller != null)
        {
            player3.SetActive(true);
            hud3.SetActive(true);
            player3.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player3Controller));
        }

        if (PhonySelectionData.Player4Controller != null)
        {
            player4.SetActive(true);
            hud4.SetActive(true);
            player4.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player4Controller));
        }

    }

    public KeysTable KeysTableFromData(String playerController)
    {
        switch (playerController)
        {
            case "Keyboard1": return Keyboard1;
            case "Keyboard2": return Keyboard2;
            case "Keyboard3": return Keyboard3;
            case "GamePad1": return Keyboard3;
        }
        return Keyboard1;
    }

}
