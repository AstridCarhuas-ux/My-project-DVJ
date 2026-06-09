using UnityEngine;

public class KillZone : MonoBehaviour
{
    [Header("UI de Caída")]
    public GameObject killZoneCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador cayó al vacío.");

            if (killZoneCanvas != null)
            {
                killZoneCanvas.SetActive(true);
            }

            Time.timeScale = 0f;

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.enabled = false;
            }
        }
    }
}