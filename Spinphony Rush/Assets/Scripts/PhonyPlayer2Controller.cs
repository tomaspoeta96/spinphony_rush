using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyPlayer2Controller : MonoBehaviour
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

    private KeysTable keys = new KeysTable("L", "I", "J", "K", "M");



    private KeyCode right;
    private KeyCode left;
    private KeyCode up;
    private KeyCode down;
    private KeyCode hability;

    private int collisionCount = 0;

    void Awake()
    {
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.RIGHT());
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.LEFT());
        up = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.UP());
        down = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.DOWN());
        hability = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.HABILITY());
    }

    void Start()
    {
        phony_body = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //print(phony_body.velocity.magnitude);
        phony_body.transform.Rotate(0f, 0f, 5f, Space.Self);

    }

    void FixedUpdate()
    {
        if (!isOnLimits())
        {
            Destroy(this.gameObject);
        }
        if (collisionCount == 0)
        {
            phony_body.AddForce(Vector3.down * 15);
        }
        if (!onPush)
        {
            addMovement(speed);
            phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed);
            if (Input.GetKey(hability))
            {
                timer += Time.deltaTime;
                seconds = (int)timer % 60;
                if (seconds >= 3) seconds = 3;
            }
            else if (Input.GetKeyUp(hability)) onPush = true;
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
                addMovement(speed);
            }
            else
            {
                phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed + 10);
                pushSpeed = speed + (10f * (seconds / 3));
                addMovement(pushSpeed);
            }

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Mapa_Arbol")
        {
            collisionCount++;
        }


    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "Mapa_Arbol")
        {
            collisionCount = 1;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Mapa_Arbol")
        {
            collisionCount--;
        }

        if (col.gameObject.name == "Phony_Player")
        {
            Vector3 vel = col.gameObject.GetComponent<Rigidbody>().velocity;
            vel = vel * (this.phony_body.velocity.magnitude * 0.2f);
            if (Mathf.Abs(this.phony_body.velocity.magnitude - col.gameObject.GetComponent<Rigidbody>().velocity.magnitude) < 2)
            {
                
            }
            else if ((this.phony_body.velocity.magnitude >= col.gameObject.GetComponent<Rigidbody>().velocity.magnitude))
            {
                col.gameObject.GetComponent<Rigidbody>().velocity = vel;
                col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(col.gameObject.GetComponent<Rigidbody>().velocity, speed);
            }
        }
    }



    private void addMovement(float speed)
    {

        if (Input.GetKey(left))
            phony_body.AddForce(Vector3.left * speed);
        if (Input.GetKey(right))
            phony_body.AddForce(Vector3.right * speed);
        if (Input.GetKey(down))
            phony_body.AddForce(Vector3.back * speed);
        if (Input.GetKey(up))
            phony_body.AddForce(Vector3.forward * speed);
    }
    private bool isOnLimits()
    {
        if (Mathf.Abs(this.gameObject.transform.position.x) <= 300 &&
           Mathf.Abs(this.gameObject.transform.position.y) <= 300 &&
           Mathf.Abs(this.gameObject.transform.position.z) <= 300)
        {

            return true;
        }
        else
        {
            print("Destroyed");
            return false;
        }
    }


}
