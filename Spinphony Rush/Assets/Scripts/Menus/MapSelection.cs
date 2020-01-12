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
    public static String mapToLoad;

    // Start is called before the first frame update
    void Start()
    {
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
        mapToLoad = "Map" + UnityEngine.Random.Range(1, maps.Length);
        SceneManager.LoadScene("LoadScreen");
    }

    private void ActionMap1()
    {
        mapToLoad = "Map1";
        SceneManager.LoadScene("LoadScreen");
    }

    private void ActionMap2()
    {
        mapToLoad = "Map2";
        SceneManager.LoadScene("LoadScreen");
    }

    void Return()
    {
        SceneManager.UnloadSceneAsync(this.gameObject.scene);
        if (GameRules.endGame)
        {
            SceneManager.LoadScene("Phony Selection");
            GameRules.endGame = false;
        }       
    }

}
