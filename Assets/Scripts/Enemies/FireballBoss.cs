using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBoss : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 2; // Da�o por segundo que inflige el fuego
    [SerializeField] private float damageInterval = 0.5f; // Intervalo en segundos entre da�os
    private bool isPlayerInTrigger = false; // Para rastrear si el jugador est� dentro del �rea
    private Coroutine damageCoroutine; // Referencia a la rutina que aplica da�o

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;

            // Inicia el da�o continuo si no est� ya en ejecuci�n
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

            // Detiene el da�o continuo si el jugador sale del �rea
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyDamageOverTime(PlayerHealth playerHealth)
    {
        while (isPlayerInTrigger) // Mientras el jugador est� en el �rea
        {
            if (playerHealth != null) // Aseg�rate de que el jugador tenga el script de salud
            {
                playerHealth.TakeDamage(damagePerSecond); // Aplica da�o
            }
            yield return new WaitForSeconds(damageInterval); // Espera el intervalo antes de volver a aplicar da�o
        }
    }
}
