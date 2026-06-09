using UnityEngine;
using System.Collections;

public class ControladorMuerte : MonoBehaviour
{
    [Header("Sonido de Muerte")]
    public AudioClip gritoHombre;
    [Range(0f, 1f)] public float volumenGrito = 0.8f;

    [Header("Música de Fondo")]
    public AudioSource musicaFondo;

    private void OnEnable()
    {
        if (musicaFondo != null)
        {
            musicaFondo.Stop();
        }

        AudioListener.pause = true;

        if (gritoHombre != null)
        {
            AudioSource.PlayClipAtPoint(gritoHombre, Camera.main.transform.position, volumenGrito);
            
            GameObject sonidoTemporal = GameObject.Find("OneShotAudio");
            if (sonidoTemporal != null)
            {
                AudioSource src = sonidoTemporal.GetComponent<AudioSource>();
                if (src != null) src.ignoreListenerPause = true;
            }
        }
    }
}