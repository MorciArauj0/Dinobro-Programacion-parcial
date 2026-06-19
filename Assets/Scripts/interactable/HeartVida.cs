using UnityEngine;

public class HeartVida : MonoBehaviour
{

    [SerializeField] private int cantidadCuracion;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.curarVida(cantidadCuracion);
            Destroy(gameObject);
        }
    }

}
