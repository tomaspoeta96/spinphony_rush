using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController {

    private string name;
    private int score;
    private int record;
    //private KeysTable keys = KeysTable();
    private float moveHorizontal = 0;
    private float moveVertical = 0;

    public float getMoveHorizontal() {
        return moveHorizontal;
    }

    public float getMoveVertical() {
        return moveVertical;
    }

}