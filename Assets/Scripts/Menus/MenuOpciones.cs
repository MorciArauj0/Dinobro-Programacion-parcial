using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour, IGuardable
{
    private const string PantallaCompletaKey = "PantallaCompleta";
    private const string VolumenKey = "Volumen";
    private const string CalidadKey = "Calidad";


    public AudioMixer audioMixer;


    public void Guardar()
    {
        PlayerPrefs.SetInt(PantallaCompletaKey, Screen.fullScreen ? 1 : 0);
        audioMixer.GetFloat(VolumenKey, out float volumen);
        PlayerPrefs.SetFloat(VolumenKey, volumen);
        PlayerPrefs.SetInt(CalidadKey, QualitySettings.GetQualityLevel());
        PlayerPrefs.Save();
    }

    public void Cargar()
    {
        Screen.fullScreen = PlayerPrefs.GetInt(PantallaCompletaKey, 1) == 1;
        audioMixer.SetFloat(VolumenKey, PlayerPrefs.GetFloat(VolumenKey, 0));
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(CalidadKey, 5));
    }

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
        PlayerPrefs.SetInt(PantallaCompletaKey, pantallaCompleta ? 1 : 0);
    }

    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat(VolumenKey, volumen);
        PlayerPrefs.SetFloat(VolumenKey, volumen);
    }

    public void CambiarCalidad(int calidadIndex)
    {
        QualitySettings.SetQualityLevel(calidadIndex);
        PlayerPrefs.SetInt(CalidadKey, calidadIndex);
    }

}
