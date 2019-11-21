using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyRotation : MonoBehaviour
{
    private PhonyPlayerController phony;
    private bool keysDisabler;
    // Start is called before the first frame update
    void Start()
    {
        phony = transform.GetChild(0).gameObject.GetComponent<PhonyPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        addMovement();
    }
    private void addMovement()
    {
        Vector3 currentRotation;
        if (!phony.getMuerto())
        {
            if (!phony.getKeysDisabler())
            {
                if (Input.GetKey(phony.left))
                {
                    transform.Rotate(0f, 0f, 1f, Space.Self);
                    currentRotation = transform.localRotation.eulerAngles;
                    currentRotation.z = Mathf.Clamp(currentRotation.z, -15, 15);
                    transform.localRotation = Quaternion.Euler(currentRotation);

                }
                if (Input.GetKey(phony.right)) { }
                    
                if (Input.GetKey(phony.down)) { }
                    
                if (Input.GetKey(phony.up)) { }
                    
                
                    
            }
        }
    }
}
