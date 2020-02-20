using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonyBooostShield : PhonyActionFactory
{
    public override void action() {
        phonyClass.phony_fuelle.setShield(false);
        phonyClass.setIsShield(false);
    }

    public void shield() {
        phonyClass.setHaveShield(false);
        phonyClass.setIsShield(true);
        phonyClass.phony_fuelle.setShield(true);
    }




}
