using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsData : MonoBehaviour
{
    public KeyCode[] keyFields = new KeyCode[28];
    private Options optionsScript;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        standardKeys();
    }

    // Start is called before the first frame update
    void Start()
    {
        optionsScript = GameObject.Find("Canvas").GetComponent<Options>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("Options").isLoaded)
        {
            optionsScript = GameObject.Find("Canvas").GetComponent<Options>();
        }

        for(int i = 0; i <= 27; i++)
        {
            if(optionsScript.keyFields[i].text != ""){
                keyFields[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), optionsScript.keyFields[i].text);
            }
        }
    }

    public KeysTable getKeyboard1()
    {
        return new KeysTable(keyFields[0], keyFields[1], keyFields[2], keyFields[3], keyFields[4], keyFields[5], keyFields[6]);
    }

    public KeysTable getKeyboard2()
    {
        return new KeysTable(keyFields[7], keyFields[8], keyFields[9], keyFields[10], keyFields[11], keyFields[12], keyFields[13]);
    }

    public KeysTable getKeyboard3()
    {
        return new KeysTable(keyFields[14], keyFields[15], keyFields[16], keyFields[17], keyFields[18], keyFields[19], keyFields[20]);
    }

    public KeysTable getGamePad1()
    {
        return new KeysTable(keyFields[21], keyFields[22], keyFields[23], keyFields[24], keyFields[25], keyFields[26], keyFields[27]);
    }

    void standardKeys()
    {
        keyFields[0] = KeyCode.D;
        keyFields[1] = KeyCode.W;
        keyFields[2] = KeyCode.A;
        keyFields[3] = KeyCode.S;
        keyFields[4] = KeyCode.Space;
        keyFields[5] = KeyCode.Q;
        keyFields[6] = KeyCode.E;
        keyFields[7] = KeyCode.L;
        keyFields[8] = KeyCode.I;
        keyFields[9] = KeyCode.J;
        keyFields[10] = KeyCode.K;
        keyFields[11] = KeyCode.M;
        keyFields[12] = KeyCode.U;
        keyFields[13] = KeyCode.O;
        keyFields[14] = KeyCode.Keypad6;
        keyFields[15] = KeyCode.Keypad8;
        keyFields[16] = KeyCode.Keypad4;
        keyFields[17] = KeyCode.Keypad5;
        keyFields[18] = KeyCode.Keypad0;
        keyFields[19] = KeyCode.Keypad7;
        keyFields[20] = KeyCode.Keypad9;
        keyFields[21] = KeyCode.Joystick1Button5;
        keyFields[22] = KeyCode.Joystick1Button4;
        keyFields[23] = KeyCode.Joystick1Button7;
        keyFields[24] = KeyCode.Joystick1Button6;
        keyFields[25] = KeyCode.Joystick1Button14;
        keyFields[26] = KeyCode.Joystick1Button12;
        keyFields[27] = KeyCode.Joystick1Button13;
    }
}
