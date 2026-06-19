using UnityEngine;
using UnityEngine.UI;
public class UISlider : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volumen", 0f);
    }
}
