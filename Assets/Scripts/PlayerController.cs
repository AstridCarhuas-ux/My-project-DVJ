using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Configuración de Ataque")]
    public Transform controladorGolpe;
    public float radioGolpe = 0.5f;    
    public int danoAtaque = 1;        

    [Header("Configuración de Vida y HUD")]
    public int vidaMaxima = 20; // 20 puntos = 5 corazones (cada uno vale 4)
    public int vidaActual;
    public HUDVida hudVida;

    [Header("Configuración de Inventario")]
    public int diamantesConseguidos = 0;
    public int comidaConseguida = 0;
    public HUDManager hud;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        vidaActual = vidaMaxima;

        if (hudVida != null)
        {
            hudVida.ActualizarCorazones(vidaActual);
        }
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveInput * speed));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("isJumping", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Atacar();
        }

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    void Atacar()
    {
        anim.SetTrigger("Attack");

        Collider2D[] objetosGolpeados = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colision in objetosGolpeados)
        {
            if (colision.CompareTag("Enemy")) 
            {
                ZombieIA zombie = colision.GetComponent<ZombieIA>();
                if (zombie != null)
                {
                    zombie.TomarDano(danoAtaque);
                    Debug.Log("Le pegaste al zombie");
                }
            }
        }
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        if (hudVida != null)
        {
            hudVida.ActualizarCorazones(vidaActual);
        }

        Debug.Log("¡Player dañado! Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }

        anim.SetTrigger("Hurt");
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        if (hudVida != null)
        {
            hudVida.ActualizarCorazones(vidaActual);
        }
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (controladorGolpe != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamante"))
        {
            diamantesConseguidos++;
            Destroy(other.gameObject);
            Debug.Log("¡Diamante conseguido! Total: " + diamantesConseguidos);

            if (hud != null)
            {
                hud.ActualizarMarcador(diamantesConseguidos);
            }
        }
    }
}