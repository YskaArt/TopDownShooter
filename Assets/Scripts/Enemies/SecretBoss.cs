using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBoss : MonoBehaviour
{
    [Header("Hands Settings")]
    public Transform leftHand;
    public Transform leftHandCenter;
    public Transform leftFirePoint; 
    public GameObject leftHandProjectilePrefab;
    public Transform rightHand;
    public Transform rightHandCenter;
    public Transform rightFirePoint; 
    public GameObject rightHandProjectilePrefab;
    public float projectileSpeed = 10f;
    public float handAttackInterval = 3f;

    [Header("Fire Settings")]
    public GameObject firePrefab;
    public float fireSpawnInterval = 5f;
    public int maxFires = 5;
    public Vector2 fireSpawnAreaMin;
    public Vector2 fireSpawnAreaMax;
    private AudioSource effect;
    [SerializeField] private AudioClip Faia;
    [SerializeField] private AudioClip invocation;

    [Header("Boss Movement")]
    public float moveSpeed = 2f;
    public float moveRangeX = 5f;
    private float startX;
    private bool movingRight = true;

    void Start()
    {
        startX = transform.position.x;
        effect = GetComponent<AudioSource>();
        // Iniciar las rutinas de ataque y spawn
        StartCoroutine(HandAttackRoutine());
        StartCoroutine(FireSpawnRoutine());
    }

    void Update()
    {
        // Movimiento del jefe
        BossMovement();
    }

    void BossMovement()
    {
        float targetX = movingRight ? startX + moveRangeX : startX - moveRangeX;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    IEnumerator AttackWithHand(Transform hand, Transform handCenter, Transform firePoint, GameObject handProjectilePrefab, float rotationAngle)
    {
        float attackTime = 0.5f; // Tiempo que tarda en realizar el ataque
        float elapsedTime = 0f;
        

        // Rotación del punto central de la mano
        while (elapsedTime < attackTime)
        {
            elapsedTime += Time.deltaTime;
            float targetAngle = rotationAngle * (elapsedTime / attackTime);
            handCenter.localRotation = Quaternion.Euler(0f, 0f, targetAngle);
            yield return null;
        }

        // Disparar el proyectil desde el firePoint
        effect.PlayOneShot(Faia);
        GameObject projectile = Instantiate(handProjectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (FindObjectOfType<PlayerMove>().transform.position - firePoint.position).normalized;
        rb.velocity = direction * projectileSpeed;

        yield return new WaitForSeconds(0.1f);

        // Volver el punto central a su rotación inicial
        float returnTime = 0.5f;
        float returnElapsedTime = 0f;
        float startAngle = handCenter.localRotation.eulerAngles.z;

        while (returnElapsedTime < returnTime)
        {
            returnElapsedTime += Time.deltaTime;
            float angle = Mathf.LerpAngle(startAngle, 0f, returnElapsedTime / returnTime);
            handCenter.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        handCenter.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    IEnumerator HandAttackRoutine()
    {
        while (true)
        {
            
           
            StartCoroutine(AttackWithHand(leftHand, leftHandCenter, leftFirePoint, leftHandProjectilePrefab, 60f));
            yield return new WaitForSeconds(handAttackInterval / 2f);

            StartCoroutine(AttackWithHand(rightHand, rightHandCenter, rightFirePoint, rightHandProjectilePrefab, -60f));
            yield return new WaitForSeconds(handAttackInterval);
        }
    }

    IEnumerator FireSpawnRoutine()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(fireSpawnInterval);

            effect.PlayOneShot(invocation);

            for (int i = 0; i < maxFires; i++)
            {
                float randomX = Random.Range(fireSpawnAreaMin.x, fireSpawnAreaMax.x);
                float randomY = Random.Range(fireSpawnAreaMin.y, fireSpawnAreaMax.y);

                Vector2 spawnPosition = new Vector2(randomX, randomY);

                
                Instantiate(firePrefab, spawnPosition, Quaternion.identity);

            }
        }
    }
}
