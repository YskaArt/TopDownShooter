using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretZone : MonoBehaviour
{
    public GameObject secretZone;
    void Start()
    {
        secretZone.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            secretZone.SetActive(false);

        }
    }
}
