using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    //private PhonySelector selector = PhonySelector.Instance;
    public PhonySelectionData PhonySelectionData;
    public OptionsData optionsData;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject hud1;
    public GameObject hud2;
    public GameObject hud3;
    public GameObject hud4;

    public KeysTable Keyboard1;
    public KeysTable Keyboard2;
    public KeysTable Keyboard3;
    public KeysTable GamePad1;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        if (GameObject.Find("OptionsData") != null)
        {
            optionsData = GameObject.Find("OptionsData").GetComponent<OptionsData>();
            setKeysTables();
        }
        else print("No OptionsData found, probably started without menu");

        if (GameObject.Find("PhonySelectionData") != null)
        {
            PhonySelectionData = GameObject.Find("PhonySelectionData").GetComponent<PhonySelectionData>();
            ConfigurePhonies();
        }
        else print("No PhonySelectionData found, probably started without menu");

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("OptionsData") != null)
        {
            if (optionsData == null) optionsData = GameObject.Find("OptionsData").GetComponent<OptionsData>();
            if (keysChange2())
            {
                setKeysTables();
                if(PhonySelectionData != null) setControllers();
            }
        }
    }
    
    void ConfigurePhonies()
    {
        if (PhonySelectionData.Player1Controller != null)
        {
            player1.SetActive(true);
            hud1.SetActive(true);
        }

        if (PhonySelectionData.Player2Controller != null)
        {
            player2.SetActive(true);
            hud2.SetActive(true);
        }

        if (PhonySelectionData.Player3Controller != null)
        {
            player3.SetActive(true);
            hud3.SetActive(true);
        }

        if (PhonySelectionData.Player4Controller != null)
        {
            player4.SetActive(true);
            hud4.SetActive(true);
        }

        setControllers();
    }

    public KeysTable KeysTableFromData(String playerController)
    {
        switch (playerController)
        {
            case "Keyboard1": return Keyboard1;
            case "Keyboard2": return Keyboard2;
            case "Keyboard3": return Keyboard3;
            case "GamePad1": return GamePad1;
        }
        return Keyboard1;
    }

    private bool keysChange()
    {
        if(Keyboard1 != null && Keyboard2 != null && Keyboard3 != null && GamePad1 != null)
        {
            if (Keyboard1 == optionsData.getKeyboard1() &&
                Keyboard2 == optionsData.getKeyboard2() &&
                Keyboard3 == optionsData.getKeyboard3() &&
                GamePad1 == optionsData.getGamePad1())
            {
                return false;
            }
        }
        setKeysTables();
        return true;
    }

    private bool keysChange2()
    {
        bool change = false;

        if(Keyboard1 != optionsData.getKeyboard1() ||
            Keyboard2 != optionsData.getKeyboard2() ||
            Keyboard3 != optionsData.getKeyboard3() ||
            GamePad1 != optionsData.getGamePad1())
            change = true;

        return change;
    }

    void setControllers()
    {
        if (player1.activeInHierarchy && player1.GetComponentInChildren<PhonyPlayerController>() != null)
        {
            player1.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player1Controller));
        }

        if (player2.activeInHierarchy && player2.GetComponentInChildren<PhonyPlayerController>() != null)
        {
            player2.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player2Controller));
        }

        if (player3.activeInHierarchy && player3.GetComponentInChildren<PhonyPlayerController>() != null)
        {
            player3.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player3Controller));
        }

        if (player4.activeInHierarchy && player4.GetComponentInChildren<PhonyPlayerController>() != null)
        {
            player4.GetComponentInChildren<PhonyPlayerController>().setKeys(
                KeysTableFromData(PhonySelectionData.Player4Controller));
        }
    }

    void setKeysTables()
    {
        Keyboard1 = optionsData.getKeyboard1();
        Keyboard2 = optionsData.getKeyboard2();
        Keyboard3 = optionsData.getKeyboard3();
        GamePad1 = optionsData.getGamePad1();
    }

}
