using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Referencias")]
    public TextMeshProUGUI textoDiamantes;

    public void ActualizarMarcador(int cantidad)
    {
        if (textoDiamantes != null)
        {
            textoDiamantes.text = "DIAMANTES: " + cantidad;
        }
    }
}