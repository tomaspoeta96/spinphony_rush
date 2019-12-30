using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    public Button[] maps;
    public Button BackButton;

    // Start is called before the first frame update
    void Start()
    {
        chargeOptions();
            maps[0].onClick.AddListener(RandomMap);
            maps[1].onClick.AddListener(ActionMap1);
            maps[2].onClick.AddListener(ActionMap2);
        BackButton.onClick.AddListener(Return);
    }

    private void Update()
    {
        if (GameObject.Find("OptionsData") != null && SceneManager.GetSceneByName("Options").isLoaded)
            SceneManager.UnloadSceneAsync("Options");
    }

    private void RandomMap()
    {
        chargeOptions();
        SceneManager.LoadScene("Map" + UnityEngine.Random.Range(1, maps.Length));
    }

    private void ActionMap1()
    {
        chargeOptions();
        SceneManager.LoadScene("Map1");
    }

    private void ActionMap2()
    {
        chargeOptions();
        SceneManager.LoadScene("Map2");
    }

    void Return()
    {
        SceneManager.UnloadSceneAsync(this.gameObject.scene);
    }

    void chargeOptions()
    {
        SceneManager.LoadSceneAsync("Options", LoadSceneMode.Additive);
    }
}
