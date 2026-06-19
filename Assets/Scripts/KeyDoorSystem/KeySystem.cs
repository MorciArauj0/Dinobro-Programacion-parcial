using UnityEngine;


public class KeySystem : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) return;

        if (isPickedUp)
        {
            Vector3 offset = new Vector3(0, 0.25f, 0);
            transform.position = Vector2.SmoothDamp(transform.position, Player.transform.position + offset, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            collision.gameObject.GetComponent<Player>().AgregarLlave();
        }
    }
}
