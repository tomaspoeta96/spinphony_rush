using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntagPlayer : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Phony_Player")
        {
            col.gameObject.transform.parent.tag = "Untagged";
        }
    }
}
