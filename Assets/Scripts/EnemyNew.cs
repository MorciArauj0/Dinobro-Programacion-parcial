using UnityEngine;

public class EnemyNew : Enemy
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject Bala;
    [SerializeField] private int tiempoEntreDisparos;
    [SerializeField] private int tiempoUltimoDisparo;
    [SerializeField] private int tiempoEsperaDisparo;


    void Update()
    {
        if (Time.time > tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = (int)Time.time;
        }
    }

    void Disparar()
    {
        Instantiate(Bala, controladorDisparo.position, controladorDisparo.rotation);
    }
}
