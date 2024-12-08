using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public Rigidbody2D rb;

    public float lifeTime = 20f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //obtener el componente de vida del enemigo
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        BossLife Boss = hitInfo.GetComponent<BossLife>();

        if (enemy != null)
        {
            // Si es un enemigo, le restamos 1 punto de vida
            enemy.TakeDamage(1);
            Destroy(gameObject); // Destruye la bala si golpea a un enemigo
        }
        if (Boss != null)
        {

            Boss.TakeDamage(1);
            Destroy(gameObject); // Destruye la bala si golpea a un enemigo
        }


        if (hitInfo.CompareTag("Fireball"))
        {
            
            Debug.Log("La bala ha atravesado la Fireball.");
        }
        else
        {
            
            Destroy(gameObject);
        }
        
    }
}
