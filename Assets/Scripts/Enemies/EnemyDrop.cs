using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab; // Prefab del �tem que puede caer
        [Range(0, 100)]
        public float dropChance; // Probabilidad de que este �tem caiga (0-100%)
    }

    public List<DropItem> possibleDrops = new List<DropItem>(); // Lista de �tems posibles
    public float noDropChance = 50f; // Probabilidad de que no caiga nada (0-100%)


    public void TryDropItem()
    {
        float randomValue = Random.Range(0f, 100f); // N�mero aleatorio entre 0 y 100
        float cumulativeChance = 0f;

        // Primero verifica si no cae ning�n �tem
        if (randomValue < noDropChance)
        {
            // No drop
            Debug.Log("No se solt� ning�n �tem.");
            return;
        }

        // Si no se cumple la chance de "no drop", contin�a con los posibles �tems
        foreach (DropItem drop in possibleDrops)
        {
            cumulativeChance += drop.dropChance;
            if (randomValue <= cumulativeChance)
            {
                Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log("Solt�: " + drop.itemPrefab.name);
                return; // Sale del m�todo para evitar m�ltiples drops
            }
        }

        // Por si no hay coincidencia, no se suelta nada
        Debug.Log("No se solt� ning�n �tem.");
    }
}
