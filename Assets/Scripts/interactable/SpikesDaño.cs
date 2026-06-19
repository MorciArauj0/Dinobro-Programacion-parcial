using UnityEngine;
using System.Collections;

public class SpikesDaño : MonoBehaviour
{

    [SerializeField] private int dañoPorToque;
    [SerializeField] private float bounceForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.tomarDaño(dañoPorToque);
            aplicarKnockback(collision.gameObject);
        }
    }

    private void aplicarKnockback(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.linearVelocity = Vector2.zero;

            bool golpeDesdeArriba = player.transform.position.y > transform.position.y + 0.3f;

            if (golpeDesdeArriba)
            {
                rb.gravityScale = 1f;
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
            else
            {
                float direccion = player.transform.position.x > transform.position.x ? 1f : -1f;
                rb.gravityScale = 0.5f;
                rb.AddForce(new Vector2(direccion * 4f, 3f), ForceMode2D.Impulse);
                StartCoroutine(restaurarGravedad(rb));
            }
        }
    }

    private IEnumerator restaurarGravedad(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.4f);
        if (rb != null)
        {
            rb.gravityScale = 4f;
        }
    }

}
