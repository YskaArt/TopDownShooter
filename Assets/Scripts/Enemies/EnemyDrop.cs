using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab; // Prefab del ítem que puede caer
        [Range(0, 100)]
        public float dropChance; // Probabilidad de que este ítem caiga (0-100%)
    }

    public List<DropItem> possibleDrops = new List<DropItem>(); // Lista de ítems posibles
    public float noDropChance = 50f; // Probabilidad de que no caiga nada (0-100%)


    public void TryDropItem()
    {
        float randomValue = Random.Range(0f, 100f); // Número aleatorio entre 0 y 100
        float cumulativeChance = 0f;

        // Primero verifica si no cae ningún ítem
        if (randomValue < noDropChance)
        {
            // No drop
            Debug.Log("No se soltó ningún ítem.");
            return;
        }

        // Si no se cumple la chance de "no drop", continúa con los posibles ítems
        foreach (DropItem drop in possibleDrops)
        {
            cumulativeChance += drop.dropChance;
            if (randomValue <= cumulativeChance)
            {
                Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log("Soltó: " + drop.itemPrefab.name);
                return; // Sale del método para evitar múltiples drops
            }
        }

        // Por si no hay coincidencia, no se suelta nada
        Debug.Log("No se soltó ningún ítem.");
    }
}
