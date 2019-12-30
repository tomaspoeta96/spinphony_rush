using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Button BackButton;
    public EventSystem eventSystem;
    public InputField[] keyFields;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("OptionsData") == null) CreateSelectionDataObject();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        BackButton.onClick.AddListener(Return);
        printKeys();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Return();
        }

        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                print("joystick 1 button " + i);
                InputField iF = eventSystem.currentSelectedGameObject.GetComponent<InputField>();
                iF.text = "Joystick1Button" + i;
            }
        }
    }

    void CreateSelectionDataObject()
    {
        GameObject OptionsData = new GameObject("OptionsData");
        OptionsData.AddComponent<OptionsData>();
    }

    void printKeys()
    {
        for(int i=0; i<=27; i++)
        {
            keyFields[i].placeholder.gameObject.GetComponent<Text>().text =
                GameObject.Find("OptionsData").GetComponent<OptionsData>().keyFields[i].ToString();
        }
   }

    void OnGUI()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode != KeyCode.None)
        {
            if(eventSystem.currentSelectedGameObject.name.Contains("InputField"))
            {
                InputField iF = eventSystem.currentSelectedGameObject.GetComponent<InputField>();
                iF.text = Event.current.keyCode.ToString();
            }            
        }
    }

    // Update is called once per frame
    void Return()
    {
        SceneManager.UnloadSceneAsync(this.gameObject.scene);
    }
}
