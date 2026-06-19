using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCoins : MonoBehaviour
{
    public static ControladorCoins instance;

    public int cantidadCoins;
    private int cantidadCoinsCheckpoint;

    private void Awake()
    {
        if(ControladorCoins.instance == null)
        {
            ControladorCoins.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        cantidadCoinsCheckpoint = cantidadCoins;
        ActualizarTexto();
    }

    public void ReiniciarCoins()
    {
        cantidadCoins = cantidadCoinsCheckpoint;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        GameObject obj = GameObject.Find("CoinsText");
        if (obj != null) {
            TextMeshProUGUI coinsText = obj.GetComponent<TextMeshProUGUI>();
            if(coinsText != null)
            {
                coinsText.text = cantidadCoins.ToString();
            }
        }
    }
   
    public void SumarCoins(int cantidad)
    {
        cantidadCoins += cantidad;
        ActualizarTexto();
    }

    public void ReiniciarTodo()
    {
        cantidadCoins = 0;
        cantidadCoinsCheckpoint = 0;
        ActualizarTexto();
    }

    public int GetCantidadCoins()
    {
        return cantidadCoins;
    }
}
