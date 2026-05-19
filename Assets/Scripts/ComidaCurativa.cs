using UnityEngine;

public class ComidaCurativa : MonoBehaviour
{
    [Header("Configuración de Curación")]
    public int cantidadCuracion = 2;
    public Sprite comidaConseguidaSprite;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null)
            {
                player.Curar(cantidadCuracion);
                
                player.comidaConseguida++;
                
                Debug.Log("¡Player curado! Comida total: " + player.comidaConseguida);

                GetComponent<Collider2D>().enabled = false;
                
                if(comidaConseguidaSprite != null && GetComponentInChildren<SpriteRenderer>() != null)
                {
                    GetComponentInChildren<SpriteRenderer>().sprite = comidaConseguidaSprite;
                }
                else
                {
                    if(GetComponentInChildren<SpriteRenderer>() != null)
                        GetComponentInChildren<SpriteRenderer>().enabled = false;
                }

                Destroy(gameObject, 0.5f);
            }
        }
    }
}