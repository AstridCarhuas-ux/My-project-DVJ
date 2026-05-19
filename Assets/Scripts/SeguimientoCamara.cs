using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform jugador;

    [Header("Configuración")]
    public float suavizado = 5f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (jugador != null)
        {
            Vector3 posicionDeseada = jugador.position + offset;

            Vector3 posicionSuave = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);

            transform.position = posicionSuave;
        }
    }
}