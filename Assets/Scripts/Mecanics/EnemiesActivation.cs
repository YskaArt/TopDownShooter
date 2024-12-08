using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemiesActivation : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemies; // Lista de enemigos a activar

    private void Start()
    {
   
        // Aseg�rate de que los enemigos est�n desactivados al inicio
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
           
            foreach (var enemy in enemies)
            {
                enemy.SetActive(true);
            }

           
            gameObject.SetActive(false);
        }
    }

 

}


