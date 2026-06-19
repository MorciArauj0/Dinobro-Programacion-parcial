using UnityEngine;
using TMPro;

public class UIDropdown : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TMP_Dropdown>().value = PlayerPrefs.GetInt("Calidad", 2);
    }
}
