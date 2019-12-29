using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonySelection : MonoBehaviour
{
    public Button openConfigButton;
    public GameObject fightConfiguration;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("PhonySelectionData") == null) CreateSelectionDataObject();
        openConfigButton.onClick.AddListener(openFightConfig);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void openFightConfig()
    {
        fightConfiguration.SetActive(true);
    }

    void CreateSelectionDataObject()
    {
        GameObject PhonySelectionData = new GameObject();
        PhonySelectionData.name = "PhonySelectionData";
        PhonySelectionData.AddComponent<PhonySelectionData>();
        GameObject.Instantiate(new GameObject(), transform.position, Quaternion.identity);
    }
}
