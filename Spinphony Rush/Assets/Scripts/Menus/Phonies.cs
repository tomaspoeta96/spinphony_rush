using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phonies : MonoBehaviour
{
    public Button BackButton;

    // Start is called before the first frame update
    void Start()
    {
        BackButton.onClick.AddListener(Return);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Return()
    {
        SceneManager.UnloadSceneAsync(this.gameObject.scene);
    }
}
