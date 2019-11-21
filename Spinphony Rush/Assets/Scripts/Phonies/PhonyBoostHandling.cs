using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyBoostHandling : PhonyActionFactory {

    private PhysicMaterial material;

    public override void Start() {
        base.Start();
        material = phonyClass.normalPhysic;
    }

    public override void action() {
        GetComponent<Collider>().material = material;
        phonyClass.setIsMove(false);
    }

    public void setFloorMaterial(PhysicMaterial mat) {
        material = mat;
    }
    public PhysicMaterial getFloorMaterial() {
        return material;
    }

    public void move() {
        phonyClass.setHaveMove(false);
        phonyClass.setIsMove(true);
        phonyClass.GetComponent<Collider>().material = phonyClass.movePhysic;
    }
}
