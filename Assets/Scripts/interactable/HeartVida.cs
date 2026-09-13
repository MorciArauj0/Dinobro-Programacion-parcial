using UnityEngine;

public class HeartVida : MonoBehaviour, IInteractable
{

    [SerializeField] private int cantidadCuracion;

    public void Interact(Player player)
    {
        player.curarVida(cantidadCuracion);
        Destroy(gameObject);
    }

}
