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

    public KeysTable() {

    }

    public KeysTable(string d, string w, string a, string s, string sp, string b) {
        rightKey = d;
        leftKey = a;
        upKey = w;
        downKey = s;
        habilityKey = sp;
        boostKey = b;

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



}