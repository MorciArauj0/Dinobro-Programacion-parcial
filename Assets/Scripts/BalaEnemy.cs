using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class BalaEnemy : Daño
{


    [SerializeField] private int velocidad;
    [SerializeField] private float distanciaMax;
    private GameObject gameOverPanel;
    private EnemyNew enemyPadre;
    private Vector3 posicionInicial;


    public void SetGameOverPanel(GameObject panel)
    {
        gameOverPanel = panel;
    }

    public void SetEnemyPadre(EnemyNew enemy)
    {
        enemyPadre = enemy;
    }

    void Update()
    {
        if (gameOverPanel == null || !gameOverPanel.activeSelf)
        {
            transform.Translate(Time.deltaTime * velocidad * Vector2.right);
        }

        if (Vector3.Distance(transform.position, posicionInicial) > distanciaMax)
        {
                enemyPadre.RetornarBalaAlPool(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyPadre?.RetornarBalaAlPool(this);
    }
}
