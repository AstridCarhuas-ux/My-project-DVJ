using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaCanvas;
    private bool juegoPausado = false;

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
    }

    void Pausar()
    {
        menuPausaCanvas.SetActive(true); 
        Time.timeScale = 0f; 
        juegoPausado = true;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }
}