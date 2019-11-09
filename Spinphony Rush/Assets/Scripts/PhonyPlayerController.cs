﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyPlayerController : MonoBehaviour {
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
    //private KeysTable keys = new KeysTable("L","I","J","K","M","U");
    public RandomBoost boosts;
    public Fuelle currentFuelle;
    private bool muerto = false;
    public Fuelle phony_fuelle;
    private float elapsedTime = 0f;
    private float durationTime = 10f;
    private bool isShield = false;
    private bool isJump = false;
    private bool isMove = false;

    private bool isBeaten = false;
    private float elapsedTimeBeat = 0f;
    private float durationTimeBeat = 0.1f;
    private bool keysDisabler = false;

    private bool isReverb = false;
    private float elapsedTimeReverb = 0f;
    private float durationTimeReverb = 0.8f;

    public PhysicMaterial normalPhysic;
    public PhysicMaterial movePhysic;

    public KeyCode right;
    public KeyCode left;
    public KeyCode up;
    public KeyCode down;
    public KeyCode hability;
    public KeyCode boost;
    public KeyCode reverb;

    private int collisionCount = 0;

    void Awake()
    {
        //right = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.RIGHT()) ;
        //left = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.LEFT()) ;
        //up = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.UP()) ;
        //down = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.DOWN()) ;
        //hability = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.HABILITY()) ;
        //boost = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.BOOST()) ;
    }

    void Start()
    {
        phony_body = GetComponent<Rigidbody>();
    }


    private void Update() {
        //print(phony_body.velocity.magnitude);
        if (isShield) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= durationTime) {
                phony_fuelle.shield = false;
                isShield = false;
                elapsedTime = 0;
            }
        }
        if (isMove) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= durationTime) {
                GetComponent<Collider>().material = normalPhysic;
                isMove = false;
                elapsedTime = 0;
            }
        }


        if(isJump) {
            phony_body.AddForce(Vector3.up * 1000f);
            isJump = false;
        }

        if(isReverb) {
        	elapsedTimeReverb += Time.deltaTime;
        	if(elapsedTimeReverb >= durationTimeReverb) {
        		phony_body.mass = 1f;
        		isReverb = false;
        		elapsedTimeReverb = 0f;
        	}
        }




        //si la peonza se queda sin fuelle
        if (currentFuelle.fuelleSlider.value <= 0)
        {
            muerto = true;
            muerte();
            phony_body.constraints = RigidbodyConstraints.None;
        }
        else {
            phony_body.transform.Rotate(0f, 0f, 5f, Space.Self);
            if (Input.GetKey(boost))
            {
                if (haveJump) jump();
                if (haveShield) shield();
                if (haveFuelle) fuelle();
                if (haveMove) move();
            }
        }
    }

    void FixedUpdate() {
        if(!isOnLimits()){
          Destroy(this.gameObject);
        }
        if(collisionCount == 0) {
          phony_body.AddForce(Vector3.down * 15);
        }

        if (isBeaten)
        {
            elapsedTimeBeat += Time.deltaTime;
            if (elapsedTimeBeat >= durationTimeBeat)
            {
                keysDisabler = true;
                isBeaten = false;
                elapsedTimeBeat = 0f;
            }
            if(!isBeaten)
            {
                keysDisabler = false;
            }
        }
        if (!onPush) {
            addMovement(speed);
            phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed);
            if (Input.GetKey(hability)) {
                timer += Time.deltaTime;
                seconds = (int)timer % 60;
                if (seconds >= 3) seconds = 3;
            }
            else if(Input.GetKeyUp(hability)) onPush = true;
        }

        else if (onPush) {
            
            pushTimer += Time.deltaTime;
            pushSeconds = (int)pushTimer % 60;
            if (pushSeconds >= 2) {
                seconds = 0;
                phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed);
                onPush = false;
                timer = 0f;
                pushTimer = 0f;
                pushSeconds = 0;
                addMovement(speed);
            }
            else {
                phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed + 10);
                pushSpeed = speed + (10f*(seconds/3));
                addMovement(pushSpeed);
            }

        }
    }


    void OnTriggerEnter(Collider c) {
        if(!haveFuelle && !haveJump && !haveMove && !haveShield) {
            switch (c.gameObject.name) {
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

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.name == "Mapa_Arbol") {
            phony_body.drag = 0;
            collisionCount++;
        }
        if (col.gameObject.name == "Phony_Player" || col.gameObject.name == "Phony_IA")
        {
            print("vel phony: "+ col.gameObject.GetComponent<Rigidbody>().velocity);
            print("vel enemy" + phony_body.velocity);
            print("impactdir: " + Vector3.Dot(Vector3.Normalize(phony_body.velocity.normalized), Vector3.Normalize(col.gameObject.GetComponent<Rigidbody>().velocity.normalized)));
            print("impactforce: " + col.relativeVelocity.magnitude);
            Vector3 vel = col.gameObject.GetComponent<Rigidbody>().velocity;
            vel = vel * (this.phony_body.velocity.magnitude * 0.2f);
            Physics.IgnoreCollision(col.collider, phony_body.gameObject.GetComponent<MeshCollider>(), true);
            if (!Mathf.Approximately(Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity.normalized, phony_body.velocity.normalized),1f) &&
                !Mathf.Approximately(Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity.normalized, phony_body.velocity.normalized), -1f))
            {
                if (col.relativeVelocity.magnitude > 15f)
                {
                    if ((this.phony_body.velocity.magnitude >= col.gameObject.GetComponent<Rigidbody>().velocity.magnitude))
                    {
                        phony_body.velocity = phony_body.velocity * 0.95f;
                        col.gameObject.GetComponent<Rigidbody>().AddForce(vel);
                        col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(vel, speed);
                        col.gameObject.GetComponent<PhonyPlayerController>().isBeaten = true;
                    }
                }
            }
        }
    }

    void OnCollisionStay(Collision col) {
        if(col.gameObject.name == "Mapa_Arbol") {
            collisionCount = 1;
        }
        
    }
     void OnCollisionExit(Collision col)
    {
        if(col.gameObject.name == "Mapa_Arbol") {
            phony_body.drag = 0.1f;
            collisionCount--;
        }

        if (col.gameObject.name == "Phony_Player" || col.gameObject.name == "Phony_IA")
        {
            Physics.IgnoreCollision(col.collider, phony_body.gameObject.GetComponent<MeshCollider>(), false);
        }


    }
 
     

    private void addMovement(float speed) {
        if (!muerto) {
            if (!keysDisabler) {
                if (Input.GetKey(left))
                    phony_body.AddForce(Vector3.left * speed);
                if (Input.GetKey(right))
                    phony_body.AddForce(Vector3.right * speed);
                if (Input.GetKey(down))
                    phony_body.AddForce(Vector3.back * speed);
                if (Input.GetKey(up))
                    phony_body.AddForce(Vector3.forward * speed);
                if (Input.GetKey(reverb))
                    invokeReverb();
            }
        }
    }

    private bool isOnLimits() {
        if(Mathf.Abs(this.gameObject.transform.position.x) <= 300 &&
        Mathf.Abs(this.gameObject.transform.position.y) <= 300 &&
        Mathf.Abs(this.gameObject.transform.position.z) <= 300) {
            return true;
        } else {
            print("Destroyed");
            muerte();
            return false;
      }
    }

    private void jump() {
        haveJump = false;
        isJump = true;
    }

    private void shield(){
        haveShield = false;
        isShield = true;
        phony_fuelle.shield = true;
    }

    private void fuelle(){
        phony_fuelle.fuelleSlider.value = 1;
        haveFuelle = false;
    }

    private void move(){
        haveMove = false;
        isMove = true;
        GetComponent<Collider>().material = movePhysic;
    }

    private bool muerte()
    {
        muerto = true;
        gameObject.tag = "Untagged";
        Destroy(currentFuelle.gameObject, 1.0f);
        this.enabled = false;
        print("Destroyed");
        return true;
    }

    private void invokeReverb() {
    	phony_body.mass = 10f;
    	isReverb = true;
    }

}
