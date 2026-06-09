using UnityEngine;

public class SonidoDiamante : MonoBehaviour
{
    public AudioClip audioTintineo;
    [Range(0f, 1f)] public float volumen = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioTintineo != null)
            {
                AudioSource.PlayClipAtPoint(audioTintineo, transform.position, volumen);
            }
        }
    }
}