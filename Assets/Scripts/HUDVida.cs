using UnityEngine;
using UnityEngine.UI;

public class HUDVida : MonoBehaviour
{
    public Image[] corazones; 

    public Sprite corazonLleno;
    public Sprite corazonMedio;
    public Sprite corazonVacio;

    public void ActualizarCorazones(int vidaActual)
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (vidaActual >= (i + 1) * 2)
            {
                corazones[i].sprite = corazonLleno;
            }
            else if (vidaActual == ((i + 1) * 2) - 1)
            {
                corazones[i].sprite = corazonMedio;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }
        }
    }
}