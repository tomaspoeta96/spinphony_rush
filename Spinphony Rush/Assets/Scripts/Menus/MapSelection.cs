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

    // Start is called before the first frame update
    void Start()
    {
            maps[0].onClick.AddListener(RandomMap);
            maps[1].onClick.AddListener(ActionMap1);
            maps[2].onClick.AddListener(ActionMap2);
    }

    private void RandomMap()
    {
        SceneManager.LoadScene("Map" + UnityEngine.Random.Range(1, maps.Length));
    }

    private void ActionMap1()
    {
        SceneManager.LoadScene("Map1");
    }

    private void ActionMap2()
    {
        SceneManager.LoadScene("Map2");
    }

}
