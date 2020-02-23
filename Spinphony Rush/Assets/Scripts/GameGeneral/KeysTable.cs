using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysTable {

    private KeyCode rightKey = KeyCode.D;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;
    private KeyCode habilityKey = KeyCode.P;
    private KeyCode boostKey = KeyCode.Q;
    private KeyCode reverbKey = KeyCode.E;

    public KeysTable() {

    }

    public KeysTable(KeyCode d, KeyCode w, KeyCode a, KeyCode s, KeyCode sp, KeyCode b, KeyCode r) {
        rightKey = d;
        leftKey = a;
        upKey = w;
        downKey = s;
        habilityKey = sp;
        boostKey = b;
        reverbKey = r;
    }

    public KeyCode RIGHT() {
        return rightKey;
    }
    public KeyCode LEFT() {
        return leftKey;
    }
    public KeyCode UP() {
        return upKey;
    }
    public KeyCode DOWN() {
        return downKey;
    }

    public KeyCode HABILITY() {
        return habilityKey;
    }

    public KeyCode BOOST() {
        return boostKey;
    }

    public KeyCode REVERB() {
        return reverbKey;
    }

}