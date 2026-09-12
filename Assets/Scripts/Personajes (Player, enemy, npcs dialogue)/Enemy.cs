using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //este script va a servir como herencia para todos los enemigos, por lo que no se va a instanciar directamente en la escena, sino que se va a instanciar una clase hija de esta clase
    
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float speed;
    [SerializeField] private int dañoPorToque;

    private Rigidbody2D rb;
    private Vector2 movement;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (player == null) return;

        float distanteToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanteToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);
        }
        else
        {
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.tomarDaño(dañoPorToque);
        }
    }
}
