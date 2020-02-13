﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -3.0F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, 0f, -0.3f, Space.Self);
    }
}
