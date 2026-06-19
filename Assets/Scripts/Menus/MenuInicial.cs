using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuInicial : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 1f;

        if (LevelsAudio.instance != null)
            LevelsAudio.instance.DetenerMusica();
    }

    public void Jugar()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté en su estado normal al iniciar el juego

        if (ControladorCoins.instance != null)
            ControladorCoins.instance.ReiniciarTodo();

        if (LevelsAudio.instance != null)
            LevelsAudio.instance.ReanudarMusica();

        PlayerPrefs.DeleteKey("VidaActual");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Carga la siguiente escena en el orden de construcción
        
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit(); // Cierra la aplicación
    }

}
