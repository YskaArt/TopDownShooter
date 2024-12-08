using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanderaNacional : MonoBehaviour
{
    public float speedBoostPercentage = 25f; // Incremento en porcentaje de la velocidad de movimiento
    public float fireRateBoostPercentage = 25f; // Incremento en porcentaje de la cadencia de disparo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifica que el jugador lo recoja
        {
            // Busca los scripts relevantes
            PlayerMove playerMove = collision.GetComponent<PlayerMove>();
            Shoot playerShoot = collision.GetComponent<Shoot>();

            if (playerMove != null)
            {
                // Incrementa la velocidad de movimiento permanentemente
                playerMove.Speed += playerMove.Speed * (speedBoostPercentage / 100f);
            }

            if (playerShoot != null)
            {
                // Incrementa la cadencia de disparo permanentemente
                playerShoot.fireRate -= playerShoot.fireRate * (fireRateBoostPercentage / 100f);
                playerShoot.fireRate = Mathf.Max(playerShoot.fireRate, 0.1f); // Evita valores negativos o demasiado pequeños
            }

            // Destruye el objeto recolectable
            Destroy(gameObject);
        }
    }
}
