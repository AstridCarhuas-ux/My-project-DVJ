using UnityEngine;

public class SonidoComida : MonoBehaviour
{
    [Header("Configuración de Sonido")]
    public AudioClip audioMordida;
    [Range(0f, 1f)] public float volumen = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioMordida != null)
            {
                AudioSource.PlayClipAtPoint(audioMordida, transform.position, volumen);
            }
        }
    }
}