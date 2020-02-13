using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRing_Space : MonoBehaviour
{
    private AudioSource[] sounds;
    private AudioSource peonzaCrashRing;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        peonzaCrashRing = sounds[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "Phony_Player") {
            peonzaCrashRing.Play();
        }

    }
}
