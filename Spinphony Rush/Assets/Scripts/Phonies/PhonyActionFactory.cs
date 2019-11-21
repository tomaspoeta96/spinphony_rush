using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class PhonyActionFactory : MonoBehaviour {
    protected PhonyPlayerController phonyClass;
    protected float elapsedTime = 0f;

    public virtual void Start() {
        phonyClass = GetComponent<PhonyPlayerController>();
    }

    public virtual void action() {
	
	}

    public void actionTime(bool param) {
        if (param) {
            action();
        }
    }
    
    public virtual void actionTime(bool param, float durationTime) {
        if(durationTime == 0f) {
            actionTime(param);
        }
		else if (param) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= durationTime) {
                action();
                elapsedTime = 0f;
            }
        }
	}
}