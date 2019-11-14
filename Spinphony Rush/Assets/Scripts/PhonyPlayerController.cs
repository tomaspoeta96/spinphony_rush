using System;
using System.Collections;
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
    private bool isShield = false;
    private bool isJump = false;
    private bool isMove = false;

    private bool isBeaten = false;
    private bool keysDisabler = false;
    private bool isReverb = false;

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

    private PhonyBoostHandling phonyBoostHandling;
    private PhonyBooostShield phonyBoostShield;
    private PhonyBoostJump phonyBoostJump;
    private PhonyReverb phonyReverb;
    private PhonyBeaten phonyBeaten;

    //void Awake()
    //{
        //right = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.RIGHT()) ;
        //left = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.LEFT()) ;
        //up = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.UP()) ;
        //down = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.DOWN()) ;
        //hability = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.HABILITY()) ;
        //boost = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.BOOST()) ;
    //}

    void Start() {
        phony_body = GetComponent<Rigidbody>();
        phonyBoostHandling = gameObject.AddComponent<PhonyBoostHandling>() as PhonyBoostHandling;
        phonyBoostShield = gameObject.AddComponent<PhonyBooostShield>() as PhonyBooostShield;
        phonyBoostJump = gameObject.AddComponent<PhonyBoostJump>() as PhonyBoostJump;
        phonyReverb = gameObject.AddComponent<PhonyReverb>() as PhonyReverb;
        phonyBeaten = gameObject.AddComponent<PhonyBeaten>() as PhonyBeaten;
    }


    private void Update() {
        phonyBoostShield.actionTime(isShield, 5f);
        phonyBoostHandling.actionTime(isMove, 5f);
        phonyBoostJump.actionTime(isJump);
        phonyReverb.actionTime(isReverb, 0.1f);
        phonyBeaten.actionTime(isBeaten, 0.1f);

        checkFuelle();
    }

    void FixedUpdate() {
        if(!isOnLimits()) {
          Destroy(this.gameObject);
        }
        if(collisionCount == 0) {
          phony_body.AddForce(Vector3.down * 15);
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
            phony_body.transform.Rotate(0f, 0f, 8f, Space.Self);
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
        if (col.gameObject.name == "Phony_Player" || col.gameObject.name == "Phony_IA") {
            Vector3 vel = col.gameObject.GetComponent<Rigidbody>().velocity;
            float dir = Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity.normalized, phony_body.velocity.normalized);
            vel *= (this.phony_body.velocity.magnitude * 0.2f);
            Physics.IgnoreCollision(col.collider, phony_body.gameObject.GetComponent<MeshCollider>(), true);
            if (!Mathf.Approximately(dir,1f) && !Mathf.Approximately(dir, -1f)) {
                if (col.relativeVelocity.magnitude > 15f) {
                    if ((this.phony_body.velocity.magnitude >= col.gameObject.GetComponent<Rigidbody>().velocity.magnitude)) {
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
     void OnCollisionExit(Collision col) {
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
                    phonyReverb.reverb();
            }
        }
    }

    private bool isOnLimits() {
        if(
        Mathf.Abs(this.gameObject.transform.position.x) <= 300 &&
        Mathf.Abs(this.gameObject.transform.position.y) <= 300 &&
        Mathf.Abs(this.gameObject.transform.position.z) <= 300) {
            return true;
        }
        else {
            print("Player Destroyed");
            muerte();
            return false;
        }
    }
    
    private void fuelle() {
        phony_fuelle.fuelleSlider.value = 1;
        haveFuelle = false;
    }

    private void checkFuelle() {
        //si la peonza se queda sin fuelle
        if (currentFuelle.fuelleSlider.value <= 0) {
            muerto = true;
            muerte();
            phony_body.constraints = RigidbodyConstraints.None;
        }
        else {
            phony_body.transform.Rotate(0f, 0f, 5f, Space.Self);
            if (Input.GetKey(boost)) {
                if (haveJump) phonyBoostJump.jump();
                if (haveShield) phonyBoostShield.shield();
                if (haveFuelle) fuelle();
                if (haveMove) phonyBoostHandling.move();
            }
        }
    }

    private bool muerte() {
        muerto = true;
        gameObject.tag = "Untagged";
        Destroy(currentFuelle.gameObject, 1.0f);
        this.enabled = false;
        print("Destroyed");
        return true;
    }

    public void setIsMove(bool isMove) {
        this.isMove = isMove;
    }

    public void setHaveMove(bool haveMove) {
        this.haveMove = haveMove;
    }

    public void setIsShield(bool isShield) {
        this.isShield = isShield;
    }

    public void setHaveShield(bool haveShield) {
        this.haveShield = haveShield;
    }

    public void setIsJump(bool isJump) {
        this.isJump = isJump;
    }

    public void setHaveJump(bool haveJump) {
        this.haveJump = haveJump;
    }

    public void setIsReverb(bool isReverb) {
        this.isReverb = isReverb;
    }
    public Rigidbody getPhony() {
        return phony_body;
    }

    public void setIsBeaten(bool isBeaten) {
        this.isBeaten = isBeaten;
    }
    public bool getIsBeaten() {
        return isBeaten;
    }

    public void setKeysDisabler(bool keysDisabler) {
        this.keysDisabler = keysDisabler;
    }
}
