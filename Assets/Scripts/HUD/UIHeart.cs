using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour
{
    [SerializeField] private Image imagenCorazon;
    [SerializeField] private bool estaActivo;

    public void activarCorazon()
    {
        estaActivo = true;
        GetComponent<Image>().enabled = true;
    }

    public void desactivarCorazon()
    {
        estaActivo = false;
        GetComponent<Image>().enabled = false;
    }

    public bool estaCorazonActivo() => estaActivo;

}
