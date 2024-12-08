using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D rb;

    public float lifeTime = 5f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Player"))
        {
            Debug.Log("No Pasa Na");
        }
        else 
        {
            Destroy(gameObject);
        }
        

    }
}
