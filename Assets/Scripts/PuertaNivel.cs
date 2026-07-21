using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PuertaNivel : MonoBehaviour
{
    [Header("UI de Victoria")]
    public GameObject victoryCanvas;
    public TextMeshProUGUI victoryText;

    [Header("Condiciones de Paso")]
    public int totalDiamantesEnNivel = 256; 
    public int totalComidaEnNivel = 317;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                victoryCanvas.SetActive(true);

                DefinirMensajePaso(player);

                GetComponent<Collider2D>().enabled = false;

                StartCoroutine(IrAlNivel2());
            }
        }
    }

    void DefinirMensajePaso(PlayerController player)
    {
        if (player.diamantesConseguidos >= totalDiamantesEnNivel && player.comidaConseguida >= totalComidaEnNivel)
        {
            victoryText.text = "¡Lograste superar el sector 1 con todo el botin! El interior del edificio te espera.";
        }
        else if (player.diamantesConseguidos >= totalDiamantesEnNivel)
        {
            victoryText.text = "Pasaste la puerta con mucha fortuna, no olvides mantener el estomago lleno.";
        }
        else if (player.comidaConseguida >= totalComidaEnNivel)
        {
            victoryText.text = "Pasaste la puerta con energia para rato. ¡Sigue asi!";
        }
        else
        {
            victoryText.text = "Lograste pasar la puerta... pero te falta preparacion para lo que viene.";
        }
    }

    IEnumerator IrAlNivel2()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Nivel2");
    }
}