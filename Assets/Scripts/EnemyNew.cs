using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class EnemyNew : Enemy
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private int tiempoEntreDisparos;
    [SerializeField] private int tiempoUltimoDisparo;


    [SerializeField] private int poolSize = 4;
    [SerializeField] private BalaEnemy balaEnemyPrefab;
    [SerializeField] private GameObject gameOverPanel;

    private Queue<BalaEnemy> balaPool = new Queue<BalaEnemy>();



    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < poolSize; i++)
        {
            BalaEnemy bala = Instantiate(balaEnemyPrefab);
            bala.gameObject.SetActive(false);
            bala.SetEnemyPadre(this);
            balaPool.Enqueue(bala);
        }
    }

    void Update()
    {
        if (gameOverPanel == null) return;

        if (Time.time > tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = (int)Time.time;
        }
    }

    void Disparar()
    {
        BalaEnemy balaEnemy = balaPool.Count > 0 ? balaPool.Dequeue() : Instantiate(balaEnemyPrefab);
        if (balaEnemy == null) return;

        balaEnemy.gameObject.SetActive(true);
        balaEnemy.SetGameOverPanel(gameOverPanel);
        balaEnemy.transform.position = controladorDisparo.position;
        balaEnemy.transform.rotation = controladorDisparo.rotation;
    }

    public void RetornarBalaAlPool(BalaEnemy bala)
    {
        bala.gameObject.SetActive(false);
        balaPool.Enqueue(bala);
    }
}
