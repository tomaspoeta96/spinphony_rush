using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysTable {
    
    //codigo ascii de las teclas AWSD por defecto
    private string rightKey = "D";
    private string leftKey = "A";
    private string upKey = "W";
    private string downKey = "S";

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



}