using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysTable {

    
    //codigo ascii de las teclas AWSD por defecto
    private string rightKey = "D";
    private string leftKey = "A";
    private string upKey = "W";
    private string downKey = "S";
    private string habilityKey = "P";
    private string boostKey = "Q";
    private string reverbKey = "E";

    public KeysTable() {

    }

    public KeysTable(string d, string w, string a, string s, string sp, string b, string r) {
        rightKey = d;
        leftKey = a;
        upKey = w;
        downKey = s;
        habilityKey = sp;
        boostKey = b;
        reverbKey = r;
    }

    public string RIGHT() {
        return rightKey;
    }
    public string LEFT() {
        return leftKey;
    }
    public string UP() {
        return upKey;
    }
    public string DOWN() {
        return downKey;
    }

    public string HABILITY() {
        return habilityKey;
    }

    public string BOOST() {
        return boostKey;
    }

    public string REVERB() {
        return reverbKey;
    }

}