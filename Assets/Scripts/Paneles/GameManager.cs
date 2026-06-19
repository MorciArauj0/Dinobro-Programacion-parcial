using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button reiniciarButton;
    public Button menuButton;
    private AudioSource audioSource;
    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;
    [SerializeField] private Button reiniciarButtonVictoria;
    [SerializeField] private Button menuButtonVictoria;
    [SerializeField] private Button reiniciarButtonDerrota;
    [SerializeField] private Button menuButtonDerrota;


    private bool gameOverActivo = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    void Start()
    {
        gameOverActivo = false;
        Time.timeScale = 1f;

        audioSource = GetComponent<AudioSource>();

        // Reiniciar monedas y música al iniciar el juego
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (reiniciarButton != null)
            reiniciarButton.onClick.AddListener(ReiniciarEscena);

        if (menuButton != null)
            menuButton.onClick.AddListener(IrAlMenu);

        // Configurar botones para paneles de victoria y derrota
        if (reiniciarButtonVictoria != null)
            reiniciarButtonVictoria.onClick.AddListener(ReiniciarDesdeInicio);

        if (menuButtonVictoria != null)
            menuButtonVictoria.onClick.AddListener(IrAlMenu);

        if (reiniciarButtonDerrota != null)
            reiniciarButtonDerrota.onClick.AddListener(ReiniciarDesdeInicio);

        if (menuButtonDerrota != null)
            menuButtonDerrota.onClick.AddListener(IrAlMenu);
    }

  

    public void GameOver()
    {
        if (gameOverActivo) return;

        gameOverActivo = true;

        if (ControladorCoins.instance != null)
            ControladorCoins.instance.ReiniciarCoins();

        if (LevelsAudio.instance != null)
        {
            LevelsAudio.instance.DetenerMusica();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            audioSource.Play();
        }

        if (gameOverText != null)
        {
            gameOverText.text = "Moriste :(";
        }
    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1f;

        if (audioSource != null) audioSource.Stop();

        if (LevelsAudio.instance != null)
            LevelsAudio.instance.ReanudarMusica();

        PlayerPrefs.DeleteKey("VidaActual");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReiniciarDesdeInicio()
    {
        Time.timeScale = 1f;

        if (audioSource != null) audioSource.Stop();

        if (ControladorCoins.instance != null) ControladorCoins.instance.ReiniciarTodo();
        if (LevelsAudio.instance != null) LevelsAudio.instance.ReanudarMusica();

        PlayerPrefs.DeleteKey("VidaActual");

        SceneManager.LoadScene("Nivel0"); 
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;

        if (audioSource != null) audioSource.Stop();

        if (LevelsAudio.instance != null)
            LevelsAudio.instance.DetenerMusica();

        SceneManager.LoadScene("MenuPrincipal");
    }

    public void MostrarPanelVictoria()
    {
        Time.timeScale = 0f;
        if (PanelVictoria != null)
            PanelVictoria.SetActive(true);

        if (LevelsAudio.instance != null)
            LevelsAudio.instance.CambiarMusicaVictoria();
    }

    public void MostrarPanelDerrota()
    {
        Time.timeScale = 0f;
        if (PanelDerrota != null)
            PanelDerrota.SetActive(true);

        if (LevelsAudio.instance != null)
            LevelsAudio.instance.CambiarMusicaDerrota();
    }
}
