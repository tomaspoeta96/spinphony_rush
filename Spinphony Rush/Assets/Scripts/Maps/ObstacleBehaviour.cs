using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPosition;
    private bool destroySignal = false;
    private bool destroyed = false;
    void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!destroySignal)
        {
            if (transform.position.y - startPosition.y <= 6)
            {
                transform.position += Vector3.up;
            }
        }
        else
        {
            if (transform.position.y - startPosition.y >= 0)
            {
                transform.position -= Vector3.up;
            }
            else
            {
                Destroy(this);
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.name == "Phony_Player") {
            col.collider.GetComponent<PhonyPlayerController>().setIsBeaten(true);
        }
    }

    public void setDestroySignal(bool destroySignal)
    {
        this.destroySignal = destroySignal;
    }

}
