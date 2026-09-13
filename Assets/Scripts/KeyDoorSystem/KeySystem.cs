using UnityEngine;


public class KeySystem : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Player;

    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;

    
    void Update()
    {
        if (Player == null) return;

        if (isPickedUp)
        {
            Vector3 offset = new Vector3(0, 0.25f, 0);
            transform.position = Vector2.SmoothDamp(transform.position, Player.transform.position + offset, ref vel, smoothTime);
        }
    }

    public void Interact(Player player)
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            player.AgregarLlave();
        }
    }
}            
