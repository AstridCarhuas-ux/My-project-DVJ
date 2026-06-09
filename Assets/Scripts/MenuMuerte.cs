using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerte : MonoBehaviour
{
    [Header("Sonido de Muerte (SEGURO)")]
    public AudioClip gritoHombre;
    [Range(0f, 1f)] public float volumenGrito = 0.8f;
    public AudioSource musicaFondo;

    private AudioSource miAudioSource;

    private void OnEnable()
    {
        if (musicaFondo != null)
        {
            musicaFondo.Stop();
        }

        miAudioSource = GetComponent<AudioSource>();

        if (miAudioSource != null && gritoHombre != null)
        {
            miAudioSource.ignoreListenerPause = true; 
            miAudioSource.clip = gritoHombre;
            miAudioSource.volume = volumenGrito;
            
            miAudioSource.Play();
        }

        AudioListener.pause = true;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }
}