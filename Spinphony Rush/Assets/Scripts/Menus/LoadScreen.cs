using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    private float waitTime;
    private Texture texture_;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        waitTime = 0f;
        texture_ = GameObject.Find("Canvas").transform.Find("Background").GetComponent<RawImage>().texture;
        switch (MapSelection.mapToLoad)
        {
            case "Map1":
                texture_ = Resources.Load<Texture>("LoadScreen/Charge1");
                break;
            case "Map2":
                texture_ = Resources.Load<Texture>("LoadScreen/Charge2");
                break;
            case "Map3":
                texture_ = Resources.Load<Texture>("LoadScreen/Charge3");
                break;
        }
        GameObject.Find("Canvas").transform.Find("Background").GetComponent<RawImage>().texture = texture_;
        chargeOptions();
    }
    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if (waitTime >= 3f) {
            waitTime = 0f;
            SceneManager.LoadScene(MapSelection.mapToLoad);
        }
        
    }

    void chargeOptions()
    {
        SceneManager.LoadSceneAsync("Options", LoadSceneMode.Additive);
    }

}
