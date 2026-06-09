using UnityEngine;

public class SonidoAnimacion : MonoBehaviour
{
    public AudioClip sonidoViento;
    [Range(0f, 1f)] public float volumen = 0.5f;

    public void PlayFush()
    {
        if (sonidoViento != null)
        {
            AudioSource.PlayClipAtPoint(sonidoViento, transform.position, volumen);
        }
    }
}