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
    private KeysTable keys;
    public RandomBoost boosts;
    public Fuelle currentFuelle;
    private bool muerto = false;
    public Fuelle phony_fuelle;
    private bool isShield = false;
    private bool isJump = false;
    private bool isMove = false;
    private int points;

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

    private AudioSource[] sounds;
    private AudioSource peonzaDragSound;
    private AudioSource peonzaCrashSound;

    /*void Awake() {
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.RIGHT()) ;
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.LEFT()) ;
        up = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.UP()) ;
        down = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.DOWN()) ;
        hability = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.HABILITY()) ;
        boost = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.BOOST()) ;
        reverb = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys.REVERB()) ;
    }
    */

    void Start() {

        phony_body = GetComponent<Rigidbody>();
        phonyBoostHandling = gameObject.AddComponent<PhonyBoostHandling>() as PhonyBoostHandling;
        phonyBoostShield = gameObject.AddComponent<PhonyBooostShield>() as PhonyBooostShield;
        phonyBoostJump = gameObject.AddComponent<PhonyBoostJump>() as PhonyBoostJump;
        phonyReverb = gameObject.AddComponent<PhonyReverb>() as PhonyReverb;
        phonyBeaten = gameObject.AddComponent<PhonyBeaten>() as PhonyBeaten;
        sounds = GetComponents<AudioSource>();
        peonzaDragSound = sounds[0];
        peonzaCrashSound = sounds[1];
        points = 0;
    }


    private void Update() {
        phonyBoostShield.actionTime(isShield, 5f);
        phonyBoostHandling.actionTime(isMove, 5f);
        phonyBoostJump.actionTime(isJump);
        phonyReverb.actionTime(isReverb, 0.1f);
        phonyBeaten.actionTime(isBeaten, 0.35f);

        checkFuelle();

    }

    void FixedUpdate() {
        if (phony_body.velocity.magnitude > 2 && peonzaDragSound.isPlaying == false && collisionCount > 0)
        {
            peonzaDragSound.volume = phony_body.velocity.magnitude / maxSpeed;
            peonzaDragSound.Play();
        }

        if (!isOnLimits()) {
            Destroy(this.gameObject);
        }
        if (collisionCount == 0) {
            phony_body.AddForce(Physics.gravity * 3);
        }

        if (!onPush) {
            addMovement(speed);
            phony_body.velocity = Vector3.ClampMagnitude(phony_body.velocity, maxSpeed);
            if (Input.GetKey(hability)) {
                timer += Time.deltaTime;
                seconds = (int)timer % 60;
                if (seconds >= 3) seconds = 3;
            }
            else if (Input.GetKeyUp(hability)) onPush = true;
        }

        else if (onPush) {
            phony_body.transform.GetChild(0).Rotate(0f, 8f, 0f, Space.Self);
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
                pushSpeed = speed + (10f * (seconds / 3));
                addMovement(pushSpeed);
            }
        }
    }

    void OnTriggerEnter(Collider c) {
        if (!haveFuelle && !haveJump && !haveMove && !haveShield) {
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
        if (col.gameObject.name == "Tronco") {
            phony_body.drag = 0;
            collisionCount++;
        }
        if (col.gameObject.name == "Phony_Player" || col.gameObject.name == "Phony_IA") {
            Vector3 vel = col.gameObject.GetComponent<Rigidbody>().velocity;
            if (phony_body.velocity.magnitude >= vel.magnitude)
            {
                points += 100;
            }
            float dir = Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity.normalized, phony_body.velocity.normalized);
            vel *= (this.phony_body.velocity.magnitude * 0.25f);
            Physics.IgnoreCollision(col.collider, phony_body.gameObject.GetComponent<MeshCollider>(), true);

            if (peonzaCrashSound.isPlaying == false)
            {
                print(col.relativeVelocity.magnitude);
                peonzaCrashSound.volume = col.relativeVelocity.magnitude / 70f;
                peonzaCrashSound.Play();
            }

            if (!Mathf.Approximately(dir, 1f) && !Mathf.Approximately(dir, -1f)) {
                if (col.relativeVelocity.magnitude > 15f) {
                    if ((this.phony_body.velocity.magnitude >= col.gameObject.GetComponent<Rigidbody>().velocity.magnitude)) {
                        phony_body.velocity = phony_body.velocity * 0.95f;
                        col.gameObject.GetComponent<Rigidbody>().AddForce(vel);
                        col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(vel, speed);
                        col.gameObject.GetComponent<PhonyPlayerController>().isBeaten = true;
                    }
                }
            }
            else
            {
                if (peonzaCrashSound.isPlaying == false)
                {
                    peonzaCrashSound.volume = 0.5f;
                    peonzaCrashSound.Play();
                }
            }
        }
    }

    void OnCollisionStay(Collision col) {
        if (col.gameObject.name == "Tronco") {
            collisionCount = 1;
        }
    }
    void OnCollisionExit(Collision col) {
        if (col.gameObject.name == "Tronco") {
            phony_body.drag = 0.1f;
            collisionCount--;
        }

        if (col.gameObject.name == "Phony_Player" || col.gameObject.name == "Phony_IA")
        {
            Physics.IgnoreCollision(col.collider, phony_body.gameObject.GetComponent<MeshCollider>(), false);
        }
    }

    private void addMovement(float speed) {
        Vector3 currentRotation = phony_body.transform.localRotation.eulerAngles;
        bool checkLeftAngle = (currentRotation.z <= 15 && currentRotation.z >= 0);
        bool checkRightAngle = ((360 - currentRotation.z <= 15 && 360f - currentRotation.z >= 0) || currentRotation.z == 0);
        bool condForwardAngle = (currentRotation.x <= 15 && currentRotation.x >= 0);
        bool condBackwardAngle = ((360 - currentRotation.x <= 15 && 360f - currentRotation.x >= 0) || currentRotation.x == 0);

        if (!muerto) {
            if (!keysDisabler) {
                if (Input.GetKey(left)) {
                    phony_body.AddForce(Vector3.left * speed);
                    if (checkLeftAngle) {
                        currentRotation.z += 2f;
                        currentRotation.z = Mathf.Clamp(currentRotation.z, 0, 15);
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                    else if (Mathf.Round(currentRotation.z) >= 345f) {
                        currentRotation.z += 2f;
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                }
                if (Input.GetKey(right)) {
                    phony_body.AddForce(Vector3.right * speed);
                    if (checkRightAngle) {
                        currentRotation.z -= 2f;
                        currentRotation.z = Mathf.Clamp(currentRotation.z, 345, 360);
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                    else if (Mathf.Round(currentRotation.z) <= 15f) {
                        currentRotation.z -= 2f;
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                }
                if (Input.GetKey(up)) {
                    phony_body.AddForce(Vector3.forward * speed);
                    if (condForwardAngle) {
                        currentRotation.x += 2f;
                        currentRotation.x = Mathf.Clamp(currentRotation.x, 0, 15);
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                    else if (Mathf.Round(currentRotation.x) >= 345f) {
                        currentRotation.x += 2f;
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                }
                if (Input.GetKey(down)) {
                    phony_body.AddForce(Vector3.back * speed);
                    if (condBackwardAngle) {
                        currentRotation.x -= 2;
                        currentRotation.x = Mathf.Clamp(currentRotation.x, 345, 360);
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                    else if (Mathf.Round(currentRotation.x) <= 15f) {
                        currentRotation.x -= 2f;
                        phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                    }
                }

                if (Input.GetKey(reverb)) {
                    phonyReverb.reverb();
                }

                if (!Input.GetKey(left) && !Input.GetKey(right)) {
                    if (Mathf.Round(currentRotation.z) != 0) {
                        if (Mathf.Round(currentRotation.z) <= 15) {
                            currentRotation.z -= 0.7f;
                            phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                        }
                        else if (Mathf.Round(currentRotation.z) >= 345) {
                            currentRotation.z += 0.7f;
                            phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                        }
                    }
                }
                if (!Input.GetKey(down) && !Input.GetKey(up)) {
                    if (Mathf.Round(currentRotation.x) != 0) {
                        if (Mathf.Round(currentRotation.x) <= 15) {
                            currentRotation.x -= 0.7f;
                            phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                        }
                        else if (Mathf.Round(currentRotation.x) >= 345) {
                            currentRotation.x += 0.7f;
                            phony_body.transform.localRotation = Quaternion.Euler(currentRotation);
                        }
                    }
                }
            }
        }


    }

    private bool isOnLimits() {
        if (
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
            phony_body.transform.GetChild(0).Rotate(0f, 6f, 0f, Space.Self);
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

    //Getters y setters(agregar segun se necesiten)
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
    public bool getKeysDisabler() {
        return keysDisabler;
    }
    public bool getMuerto() {
        return muerto;
    }
    public bool getHaveJump()
    {
        return haveJump;
    }

    public bool getHaveShield()
    {
        return haveShield;
    }

    public bool getHaveFuelle() {
        return haveFuelle;
    }

    public bool getHaveMove() {
        return haveMove;
    }

    public int getPoints() {
        return points;
    }
    public void setPoints(int points)
    {
        this.points = points;
    }

    public void setKeys(KeysTable k) {
        this.keys = k;
    }
}
