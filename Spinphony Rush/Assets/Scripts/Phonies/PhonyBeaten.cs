using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyBeaten : PhonyActionFactory {
    public override void action() {
        phonyClass.setKeysDisabler(true);
        phonyClass.setIsBeaten(false);
    }
    public override void actionTime(bool param, float durationTime) {
        base.actionTime(param, durationTime);
        if(!phonyClass.getIsBeaten()) {
            phonyClass.setKeysDisabler(false);
        }
    }
}
