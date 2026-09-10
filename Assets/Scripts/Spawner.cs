using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //la lista de enemigos que se van a spawnear
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 40)
        {
            SpawnNewEnemy();
            timer = 0;
        }
    }

    void SpawnNewEnemy()
    {
        int random = Random.Range(0, 3);

        Instantiate(enemies[random], transform.position, transform.rotation);
    }
}
