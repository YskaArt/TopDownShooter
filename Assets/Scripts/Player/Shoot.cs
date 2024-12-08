using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float bulletSpeed = 20.0f;
    private Animator faia;
    private AudioSource effect;
    [SerializeField] private AudioClip DisparoSonido;

    public float fireRate = 0.5f; // Tiempo entre disparos (cadencia)
    private float nextFireTime = 0f; // Temporizador para el próximo disparo

    void Start()
    {
        faia = GetComponent<Animator>();
        effect = GetComponent<AudioSource>();

        

    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Bang();
                nextFireTime = Time.time + fireRate; // Actualiza el temporizador para el siguiente disparo
            }
        }
        
    }


    void Bang()
    {
        effect.PlayOneShot(DisparoSonido);
        faia.SetTrigger("Faia");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
}