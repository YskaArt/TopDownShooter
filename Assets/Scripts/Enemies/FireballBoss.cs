using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBoss : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 2; // Daño por segundo que inflige el fuego
    [SerializeField] private float damageInterval = 0.5f; // Intervalo en segundos entre daños
    private bool isPlayerInTrigger = false; // Para rastrear si el jugador está dentro del área
    private Coroutine damageCoroutine; // Referencia a la rutina que aplica daño

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;

            // Inicia el daño continuo si no está ya en ejecución
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamageOverTime(other.GetComponent<PlayerHealth>()));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;

            // Detiene el daño continuo si el jugador sale del área
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyDamageOverTime(PlayerHealth playerHealth)
    {
        while (isPlayerInTrigger) // Mientras el jugador esté en el área
        {
            if (playerHealth != null) // Asegúrate de que el jugador tenga el script de salud
            {
                playerHealth.TakeDamage(damagePerSecond); // Aplica daño
            }
            yield return new WaitForSeconds(damageInterval); // Espera el intervalo antes de volver a aplicar daño
        }
    }
}
