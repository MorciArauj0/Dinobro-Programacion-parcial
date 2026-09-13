using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;

    public GameObject container; // Asigna el contenedor del menú de pausa en el Inspector
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Verifica si se presiona la tecla "P"
        {
            if (!gameOverPanel.activeSelf && !PanelVictoria.activeSelf && !PanelDerrota.activeSelf)
            {
                container.SetActive(true);
                Time.timeScale = 0; //pausa
            }
        }
        
    }

        public void ButtonResume()
        {
        Time.timeScale = 1f;
        container.SetActive(false);
        }
    
        public void ButtonRestart()
        {
        Time.timeScale = 1f;

        if (ControladorCoins.instance != null)
            ControladorCoins.instance.ReiniciarCoins();

        PlayerPrefs.DeleteKey("VidaActual");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
        }

        public void ButtonMainMenu()
        {
        Time.timeScale = 1f;

        if (LevelsAudio.instance != null)
            LevelsAudio.instance.DetenerMusica();

        SceneManager.LoadScene("MenuPrincipal"); // Carga la escena del menú principal
        }
}
