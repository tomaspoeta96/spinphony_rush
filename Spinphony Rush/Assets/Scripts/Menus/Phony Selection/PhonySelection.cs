using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonySelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("PhonySelectionData") == null) CreateSelectionDataObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSelectionDataObject()
    {
        GameObject PhonySelectionData = new GameObject();
        PhonySelectionData.name = "PhonySelectionData";
        PhonySelectionData.AddComponent<PhonySelectionData>();
        GameObject.Instantiate(new GameObject(), transform.position, Quaternion.identity);
    }
}
