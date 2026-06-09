using UnityEngine;

public class ZombieIA : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 2f;
    public float distanciaDeteccion = 5f;
    public float distanciaAtaque = 1.5f; 
    public int salud = 3;                
    
    [Header("Configuración de Ataque")]
    public int danoEnemigo = 1;
    public float tiempoEntreAtaques = 1.5f;
    private float siguienteAtaque;

    [Header("Efectos de Sonido (NUEVO)")]
    public AudioClip sonidoGolpePuno;
    [Range(0f, 1f)] public float volumenGolpe = 0.6f;
    [Space]
    public AudioClip sonidoMuerteZombie;
    [Range(0f, 1f)] public float volumenMuerte = 0.7f;
    [Space]
    public AudioClip sonidoDolorPlayer;
    [Range(0f, 1f)] public float volumenDolorPlayer = 0.6f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool estaMuerto = false;
    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        if (jugador == null)
        {
            GameObject jugadorObjetivo = GameObject.FindGameObjectWithTag("Player");
            if (jugadorObjetivo != null)
            {
                jugador = jugadorObjetivo.transform;
                playerController = jugadorObjetivo.GetComponent<PlayerController>();
            }
        }
        else
        {
            playerController = jugador.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (estaMuerto || jugador == null) return; 

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia < distanciaAtaque)
        {
            if (Time.time >= siguienteAtaque)
            {
                Atacar();
                siguienteAtaque = Time.time + tiempoEntreAtaques;
            }
            else
            {
                Detenerse();
            }
        }
        else if (distancia < distanciaDeteccion)
        {
            MoverHaciaJugador();
        }
        else
        {
            Detenerse();
        }
    }

    void Atacar()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.SetFloat("Speed", 0f);
        
        anim.SetTrigger("Attack"); 

        if (playerController != null)
        {
            playerController.RecibirDano(danoEnemigo);

            if (sonidoDolorPlayer != null)
            {
                AudioSource.PlayClipAtPoint(sonidoDolorPlayer, jugador.position, volumenDolorPlayer);
            }
        }
    }

    public void TomarDano(int cantidad)
    {
        if (estaMuerto) return;

        salud -= cantidad;

        if (sonidoGolpePuno != null)
        {
            AudioSource.PlayClipAtPoint(sonidoGolpePuno, transform.position, volumenGolpe);
        }

        if (salud <= 0)
        {
            Muerte();
        }
        else
        {
            anim.SetTrigger("Hurt");
        }
    }

    void Muerte()
    {
        estaMuerto = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        anim.SetTrigger("Die"); 

        AudioSource sonidoAmbiental = GetComponent<AudioSource>();
        if (sonidoAmbiental != null)
        {
            sonidoAmbiental.Stop(); 
        }

        if (sonidoMuerteZombie != null)
        {
            AudioSource.PlayClipAtPoint(sonidoMuerteZombie, transform.position, volumenMuerte);
        }
        
        if(GetComponent<CapsuleCollider2D>() != null)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        
        this.enabled = false;
        Destroy(gameObject, 2.5f);
    }

    void MoverHaciaJugador()
    {
        float direccion = (jugador.position.x > transform.position.x) ? 1 : -1;
        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);
        anim.SetFloat("Speed", 1f);

        Vector3 escalaLocal = transform.localScale;
        escalaLocal.x = direccion;
        transform.localScale = escalaLocal;
    }

    void Detenerse()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.SetFloat("Speed", 0f);
    }
}