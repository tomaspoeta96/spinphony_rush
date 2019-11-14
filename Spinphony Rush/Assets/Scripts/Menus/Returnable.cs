using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Returnable : MonoBehaviour
{
    public KeysTable KeysTable;
    public Button BackButton;

    // Start is called before the first frame update
    void Start()
    {
        BackButton.onClick.AddListener(Return);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)){
            Return();
        }
    }

    // Update is called once per frame
    void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
