using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Slider>().onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Slider>().value = AudioListener.volume;
    }

    public void OnValueChanged()
    {
        AudioListener.volume = this.gameObject.GetComponent<Slider>().value;
    }
}
