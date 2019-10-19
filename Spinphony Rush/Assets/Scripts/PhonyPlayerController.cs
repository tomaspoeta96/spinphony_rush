using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyPlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    private Rigidbody phony_body;
    //====no estan en uso====
    /*float timeZeroToMax = 2.5f;
    float accelerationRate;
    float forwardVelocity;
    float backwardVelocity;
    float breakFactor = 3f;*/
    void Start()
    {
        phony_body = GetComponent<Rigidbody>();
        //backwardVelocity = 0f;
        //forwardVelocity = 0f;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        phony_body.AddForce(movement * speed);

        /*if (Input.GetKey(KeyCode.W))
        {
            if (backwardVelocity > 0f)
            {
                phony_body.AddForce(-breakFactor * phony_body.velocity);
                if (phony_body.velocity.magnitude == 0f)
                {
                    phony_body.velocity = Vector3.zero;
                    phony_body.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                accelerationRate = maxSpeed / timeZeroToMax;
                forwardVelocity += accelerationRate * Time.deltaTime;
                forwardVelocity = Mathf.Min(Mathf.Abs(forwardVelocity), maxSpeed);
                phony_body.velocity = transform.forward * forwardVelocity;
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            if (forwardVelocity > 0f)
            {
                phony_body.AddForce(-breakFactor * phony_body.velocity);
                if (phony_body.velocity.magnitude == 0f)
                {
                    accelerationRate = maxSpeed / timeZeroToMax;
                    forwardVelocity = 0f;
                }
            }
            else
            {
                accelerationRate = maxSpeed / timeZeroToMax;
                forwardVelocity -= accelerationRate * Time.deltaTime;
                forwardVelocity = Mathf.Min(Mathf.Abs(forwardVelocity), maxSpeed);
                phony_body.velocity = transform.forward * -forwardVelocity;
            }
        }*/

        if (phony_body.velocity.magnitude > maxSpeed)
        {
            phony_body.velocity = phony_body.velocity.normalized * maxSpeed;
        }

    }
}
