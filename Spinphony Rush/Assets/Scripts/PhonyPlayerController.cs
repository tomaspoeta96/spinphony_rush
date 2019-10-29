using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyPlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    private Rigidbody phony_body;
    private float timer = 0.0f;
    private float pushTimer;
    private int seconds;
    private int pushSeconds;
    private float pushSpeed;
    private bool onPush = false;
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

    private void Update()
    {
        print(phony_body.velocity.magnitude);
        phony_body.transform.Rotate(0f, 0f, 5f, Space.Self);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (!onPush)
        {
            phony_body.AddForce(movement * speed);
            phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed);
            if (Input.GetKey(KeyCode.Space))
            {
                timer += Time.deltaTime;
                seconds = (int)timer % 60;
                if (seconds >= 3)
                {
                    seconds = 3;
                }
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                onPush = true;
            }
        }

        else if (onPush)
        {
            
            pushTimer += Time.deltaTime;
            pushSeconds = (int)pushTimer % 60;
            if (pushSeconds >= 2)
            {
                seconds = 0;
                phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed);
                onPush = false;
                timer = 0f;
                pushTimer = 0f;
                pushSeconds = 0;
                phony_body.AddForce(movement * speed);
            }
            else
            {
                phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed + 10);
                pushSpeed = speed + (10f*(seconds/3));
                phony_body.AddForce(movement * pushSpeed);
            }
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

        }
    }

}
