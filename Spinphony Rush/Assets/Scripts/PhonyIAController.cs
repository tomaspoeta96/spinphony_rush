using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyIAController : MonoBehaviour
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
    private bool haveJump = false;
    private bool haveShield = false;
    private bool haveFuelle = false;
    private bool haveMove = false;
    public RandomBoost boosts;
    public Fuelle currentFuelle;
    private bool muerto = false;
    
    //ia
    private int estado = 0;
    private bool panico = false;
    private int umbral = 10;
    private int iteracion = 1;
    private bool hability = false;
    private bool boost = false;
    private int objetivo = 0;

    private int collisionCount = 0;

    void Start()
    {
        phony_body = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        //print(phony_body.velocity.magnitude);


        //si la peonza se queda sin fuelle
        if (currentFuelle.fuelleSlider.value <= 0)
        {
            muerto = true;
            muerte();
            gameObject.tag = "Untagged";
            phony_body.constraints = RigidbodyConstraints.None;
        }
        else
        {
            phony_body.transform.Rotate(0f, 0f, 5f, Space.Self);
            if (boost)
            {
                boost = false;
                if (haveJump) jump();
                if (haveShield) shield();
                if (haveFuelle) fuelle();
                if (haveMove) move();
            }
        }
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
            if (hability)
            {
                hability = false;
                timer += Time.deltaTime;
                seconds = (int)timer % 60;
                if (seconds >= 3) seconds = 3;
            }
            else onPush = true;
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


    void OnTriggerEnter(Collider c)
    {
        if (!haveFuelle && !haveJump && !haveMove && !haveShield)
        {
            switch (c.gameObject.name)
            {
                case "JumpBoost":
                    haveJump = true;
                    boosts.on_map_Jump = false;
                    c.gameObject.SetActive(false);
                    break;
                case "ShieldBoost":
                    haveShield = true;
                    boosts.on_map_Shield = false;
                    c.gameObject.SetActive(false);
                    break;
                case "FuelleBoost":
                    haveFuelle = true;
                    boosts.on_map_Fuelle = false;
                    c.gameObject.SetActive(false);
                    break;
                case "MoveBoost":
                    haveMove = true;
                    boosts.on_map_Move = false;
                    c.gameObject.SetActive(false);
                    break;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Mapa_Arbol")
        {
            collisionCount++;
        }

        if (col.gameObject.name == GameObject.FindGameObjectWithTag("Player").name)
        {
            estado++;
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
            vel = vel * (this.phony_body.velocity.magnitude * 0.3f);
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
        if (!muerto)
        {
            movimientoAutomatico(speed);
        }
    }

    private void movimientoAutomatico(float speed)
    {
        if (haveJump) boost = true;
        if (haveShield) boost = true;
        if (haveFuelle) boost = true;
        if (haveMove) boost = true;


        if (estado >= 0)
        {
            //no esta en uso aun
            objetivo = Random.Range(1, GameObject.FindGameObjectsWithTag("Player").Length);
            Transform target;
            GameObject t = GameObject.FindGameObjectsWithTag("Player")[0];
            if (t != this.gameObject) {
                target = t.transform;
            }
            else
            {
                t = GameObject.FindGameObjectsWithTag("Player")[1];
                target = t.transform;
            }



            print(GameObject.FindGameObjectsWithTag("Player"));

            Vector3 atacar = Vector3.Normalize(target.position - phony_body.position);
            Vector3 huir = Vector3.Normalize(phony_body.position - target.position);
            Vector3 centro = Vector3.Normalize(Vector3.zero - phony_body.position);

            if (phony_body.position.x > 10 || phony_body.position.z > 10 || phony_body.position.x < -10 || phony_body.position.z < -10)
            {
                phony_body.AddForce(((centro + atacar) / 2) * speed);
            }

            else
            {
                if (estado == 0)
                {
                    panico = false;
                }
                if (estado > umbral)
                {
                    panico = true;
                }
                if (estado < umbral && !panico)
                {
                    phony_body.AddForce(atacar * speed);
                }
                else
                {
                    phony_body.AddForce(huir * speed);
                    estado--;
                }
            }
        }
        //print(estado);
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
            muerte();
            return false;
        }
    }

    private bool muerte()
    {
        muerto = true;
        gameObject.tag = "Untagged";
        Destroy(currentFuelle.gameObject, 1.0f);
        print("Destroyed");
        this.enabled = false;
        return true;
    }

    private void jump()
    {
        haveJump = false;
    }

    private void shield()
    {
        haveShield = false;
    }

    private void fuelle()
    {
        haveFuelle = false;
    }

    private void move()
    {
        haveMove = false;
    }



}



