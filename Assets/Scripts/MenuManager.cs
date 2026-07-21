using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void EmpezarJuego()
    {
        SceneManager.LoadScene(1); 
    }

    public void SalirDelJuego()
    {
        Debug.Log("El jugador ha salido del juego.");

        #if UNITY_WEBGL
            SceneManager.LoadScene(0);
        #else
            Application.Quit(); 
        #endif
    }
}