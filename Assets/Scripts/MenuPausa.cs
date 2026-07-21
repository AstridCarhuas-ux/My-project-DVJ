using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaCanvas;
    private bool juegoPausado = false;
    private AudioSource[] todosLosAudios;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausaCanvas.SetActive(false);
        Time.timeScale = 1f;            
        juegoPausado = false;

        AudioListener.pause = false;

        todosLosAudios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in todosLosAudios)
        {
            if (audio != null)
            {
                audio.UnPause();
            }
        }
    }

    void Pausar()
    {
        menuPausaCanvas.SetActive(true); 
        Time.timeScale = 0f; 
        juegoPausado = true;

        todosLosAudios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in todosLosAudios)
        {
            if (audio != null)
            {
                audio.Pause();
            }
        }

        AudioListener.pause = true;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f; 
        AudioListener.pause = false;

        SceneManager.LoadScene(0);
    }
}