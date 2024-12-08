using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            FindObjectOfType<ScoreManager>().AddScoreFromObject(points);
            Destroy(gameObject); // Destruye el objeto recolectable
        }
    }
}
