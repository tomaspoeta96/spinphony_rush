using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyBoostJump : PhonyActionFactory
{
    public override void action() {
        phonyClass.getPhony().AddForce(Vector3.up * GameObject.Find("RandomBoost").gameObject.GetComponent<RandomBoost>().FORCE_JUMP);
        phonyClass.setIsJump(false);
    }

    public void jump() {
        phonyClass.setHaveJump(false);
        phonyClass.setIsJump(true);
    }
}