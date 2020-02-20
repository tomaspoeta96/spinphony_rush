using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GIF : MonoBehaviour
{
    private Object[] frames;
    public int fps = 24;

    private void Start() {
        frames = Resources.LoadAll("LoadScreen/LoadingGifLogo/", typeof(Texture2D));
        print(frames.Length);
    }

    void Update()
    {
        int index = (int)(Time.time * fps) % frames.Length;
        GetComponent<RawImage>().texture = (Texture2D)frames[index];
    }



}
