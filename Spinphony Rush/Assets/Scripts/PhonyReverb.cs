using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyReverb : PhonyActionFactory {
    public override void action() {
        phonyClass.getPhony().mass = 1f;
        phonyClass.setIsReverb(false);
    }

    public void reverb() {
        phonyClass.getPhony().mass = 10f;
        phonyClass.setIsReverb(true);
    }
}

