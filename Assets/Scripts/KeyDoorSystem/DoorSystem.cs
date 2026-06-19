using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSystem : MonoBehaviour
{
    public bool locked;
    [SerializeField] private GameObject player;
    [SerializeField] private bool ultimoNivel = false;
    private bool transitioning = false;

    
    void Start()
    {
        locked = true;
    }


    void Update()
    {
        if (player == null) return;
        if (transitioning) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (!locked && distance < 0.5f)
        {
            transitioning = true;

            if (ultimoNivel)
            {
                GameManager gameManager = FindAnyObjectByType<GameManager>();
                if (gameManager != null)
                    gameManager.MostrarPanelVictoria();
            }
            else
            {
                if (SceneController.instance != null)
                    SceneController.instance.NextScene();
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            locked = false;
        }
    }
}
