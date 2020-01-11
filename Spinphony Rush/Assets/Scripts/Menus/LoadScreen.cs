using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    private float waitTime = 0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if (waitTime >= 3f) {
            SceneManager.LoadScene(MapSelection.mapToLoad);
        }
    }
}
