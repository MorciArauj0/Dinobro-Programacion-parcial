using UnityEngine;

public class Coins : MonoBehaviour, IInteractable
{

    public void Interact(Player player)
    {
        ControladorCoins.instance.SumarCoins(1);
        Destroy(gameObject);
    }

}
