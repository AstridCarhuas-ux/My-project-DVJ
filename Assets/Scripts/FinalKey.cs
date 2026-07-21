using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinalKey : MonoBehaviour
{
    [Header("UI de Victoria")]
    public GameObject victoryCanvas;
    public TextMeshProUGUI victoryText;

    [Header("Condiciones de Final Perfecto")]
    [Tooltip("Suma todos los diamantes que hay en el mapa")]
    public int totalDiamantesEnNivel = 900; 
    [Tooltip("Suma todas las comidas (manzana, pan, pescado, carne) que hay en el mapa")]
    public int totalComidaEnNivel = 1118;

    [Header("Animación por Código")]
    public float velocidadFlotar = 3f;
    public float alturaFlotar = 0.2f;
    public float velocidadGiro = 100f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotar) * alturaFlotar;
        transform.position = new Vector3(posicionInicial.x, nuevoY, posicionInicial.z);

        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                victoryCanvas.SetActive(true);

                DefinirMensajeFinal(player);

                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

                StartCoroutine(RegresarAlMenu());
            }
        }
    }

    void DefinirMensajeFinal(PlayerController player)
    {
        if (player.diamantesConseguidos >= totalDiamantesEnNivel && player.comidaConseguida >= totalComidaEnNivel)
        {
            victoryText.text = "Lograste conseguir un refugio, con los bolsillos llenos de diamantes y el estomago lleno. Bien hecho, sobreviviente!";
        }
        else if (player.diamantesConseguidos >= totalDiamantesEnNivel)
        {
            victoryText.text = "Lograste sobrevivir, y con una fortuna... lastima que en el apocalipsis el dinero no tenga mucho valor.";
        }
        else if (player.comidaConseguida >= totalComidaEnNivel)
        {
            victoryText.text = "Lograste sobrevivir, por lo menos con el estomago lleno.";
        }
        else
        {
            victoryText.text = "Lograste protegerte... por ahora.";
        }
    }

    IEnumerator RegresarAlMenu()
    {
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene(0); 
    }
}