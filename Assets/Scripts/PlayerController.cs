using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            anim.SetTrigger("Attack");
        }

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
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
        Destroy(other.gameObject);
        
        Debug.Log("¡Diamante conseguido!");
    }
}
}
