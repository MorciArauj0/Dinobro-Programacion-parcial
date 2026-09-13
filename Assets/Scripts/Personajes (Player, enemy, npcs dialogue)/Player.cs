using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    //personaje
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;

    //movimiento
    private float move;
    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadRebote;

    //salto
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask GroundLayer;

    //animaciones
    [SerializeField] private Animator animator;

    //interaccion con monedas
    [SerializeField] private int cantidadCoins;

    //interracion con llave (texto)
    public int Key = 0;
    [SerializeField] private TMPro.TextMeshProUGUI keyText;

    //sonidos
    public AudioSource audioSource;

    //eventos de daño y curación
    public Action<int> jugadorTomoDaño;
    public Action<int> jugadorSeCuro;

    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Time.timeScale = 1f;

        //if (coinsText == null)
        //    coinsText = GameObject.Find("CoinsText").GetComponent<TMP_Text>();

        if (keyText == null)
            keyText = GameObject.Find("KeyText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    
    void Update()
    {
        //movimiento horizontal
        move = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        //salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }

        //animaciones
        animator.SetFloat("Speed", Mathf.Abs(move));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }

        bool isCrouching = Input.GetKey(KeyCode.LeftShift);
        float moveX = Input.GetAxisRaw("Horizontal");

        animator.SetBool("isCrouching", isCrouching);

        bool isSneaking = isCrouching && moveX != 0;
        animator.SetBool("isSneaking", isSneaking);

    }

    //verifica si el personaje esta tocando el suelo
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, GroundLayer);
    }

    //vida del personaje
    private void Awake()
    {
        if (PlayerPrefs.HasKey("VidaActual"))
            vidaActual = PlayerPrefs.GetInt("VidaActual");
        else
            vidaActual = vidaMaxima;
    }

    public void tomarDaño(int daño)
    {
        int vidaTemporal = vidaActual - daño;
        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, vidaMaxima);
        vidaActual = vidaTemporal;
        PlayerPrefs.SetInt("VidaActual", vidaActual);

        animator.SetTrigger("isHurt");

        jugadorTomoDaño?.Invoke(vidaActual);

        if (vidaActual <= 0)
        {
            destruirJugador();
            GameManager.instance.GameOver();
        }
    }

    private void destruirJugador()
    {
        Destroy(gameObject);
    }

    public void curarVida(int curacion)
    {
        int vidaTemporal = vidaActual + curacion;
        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, vidaMaxima);
        vidaActual = vidaTemporal;
        PlayerPrefs.SetInt("VidaActual", vidaActual);

        jugadorSeCuro?.Invoke(vidaActual);
    }

    public int getVidaMaxima() => vidaMaxima;
    public int getVidaActual() => vidaActual;

    public void Rebote(Vector2 puntoGolpe)
    {
        rb2D.linearVelocity = new Vector2(-velocidadRebote.x * (transform.position.x < puntoGolpe.x ? -1 : 1), velocidadRebote.y);
   }


    //interaccion con objetos   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }

    //interracion con llave (texto)
    public void AgregarLlave()
    {
        Key++;
        if (keyText != null)
        keyText.text = Key.ToString();
    }

    //interaccion con trampolines
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce * 1.1f);
        }
    }

}
