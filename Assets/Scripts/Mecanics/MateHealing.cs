using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateHealing : MonoBehaviour
{
    [SerializeField] private int healingAmount ; // Cantidad de vida que cura
    [SerializeField] private int points; // Puntos que otorga si la vida está al máximo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            // Busca el componente PlayerHealth del jugador
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (playerHealth.IsAtMaxHealth()) // Si tiene vida máxima
                {
                    // Da puntos al ScoreManager
                    FindObjectOfType<ScoreManager>().AddScoreFromObject(points);
                }
                else
                {
                    // Cura al jugador
                    playerHealth.Heal(healingAmount);
                }
            }

           
            Destroy(gameObject);
        }
    }
}
