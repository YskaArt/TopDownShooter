using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSortingOrder : MonoBehaviour
{
    [SerializeField] private int newSortingOrder = 3;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = newSortingOrder;
           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //  Restablecer el orden al salir del trigger
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 0; 
           
        }

    }
}
