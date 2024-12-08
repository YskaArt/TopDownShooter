using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ghost : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerHealth playerHealth;
    private Transform playerTransform;
    public Transform firePoint;
    public GameObject projectilePrefab; 
    public float shootCooldown = 2f; // Tiempo entre disparos
    public float moveSpeed = 2f; 
    public float shootRange = 8f; // Rango para disparar
    public float keepDistance = 6f; // Distancia mínima al jugador
    private float shootTimer; // Temporizador de disparo


    public Vector2 firePointOffsetRight = new Vector2(0.36f, -0.104f); 
    public Vector2 firePointOffsetLeft = new Vector2(-0.36f, -0.104f);

    private AudioSource effect;
    [SerializeField] private AudioClip fireballCast;



    private float stunnedTime = 2f;
    private Rigidbody2D rb;
    public enum GhostState
    {
        Idle, MovingToDistance, Shooting, Stunned
    }
    private GhostState currentState; 

    private Vector2 directionToPlayer; 
    

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        effect = GetComponent<AudioSource>();

        if (player != null)
        {
            // Obtener el componente PlayerHealth del objeto encontrado
            playerHealth = player.GetComponent<PlayerHealth>();

            // Obtener el Transform del objeto encontrado
            playerTransform = player.transform;
        }

        rb = GetComponent<Rigidbody2D>();
        
        currentState = GhostState.Idle;
        shootTimer = shootCooldown; // Inicializa el temporizador de disparo
    }

    void Update()
    {
        // Determina la distancia y dirección al jugador
        directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Cambia de estado según la distancia
        switch (currentState)
        {
            case GhostState.Idle:
                
               
                    currentState = GhostState.MovingToDistance;
                
                break;

            case GhostState.MovingToDistance:
                // Se mueve hacia la distancia adecuada
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position + (Vector3)directionToPlayer * keepDistance, moveSpeed * Time.deltaTime);

                // Si está a la distancia correcta, comienza a disparar
                if (Vector2.Distance(transform.position, playerTransform.position) >= keepDistance)
                {
                    currentState = GhostState.Shooting;
                }
                break;

            case GhostState.Shooting:
                // Dispara proyectiles si el tiempo lo permite
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0)
                {
                    Shoot();
                    shootTimer = shootCooldown; // Reinicia el temporizador
                }

                // Si el jugador sale del rango de disparo, vuelve a moverse
                if (distanceToPlayer > shootRange)
                {
                    currentState = GhostState.MovingToDistance;
                }
                break;
            
            case GhostState.Stunned: 
                rb.velocity = Vector2.zero; // Detener el movimiento
                break;
        }

        // Rota el Ghost en función de la dirección del jugador
        RotateTowardsPlayer();
    }

    void Shoot()
    {
        // Instancia el proyectil y lo orienta hacia el jugador
        effect.PlayOneShot(fireballCast);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f; // La velocidad del proyectil puede ajustarse
    }

    void RotateTowardsPlayer()
    {
        
        if (directionToPlayer.x > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
            spriteRenderer.flipX = false;
        }
        else if (directionToPlayer.x < 0)
        {

            transform.localScale = new Vector3(2, 2, 2);
            spriteRenderer.flipX = true;
        }
        if (spriteRenderer != null && firePoint != null)
        {
            firePoint.localPosition = spriteRenderer.flipX ? firePointOffsetLeft : firePointOffsetRight;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambiar al estado Stunned por unos segundos
            StartCoroutine(HandleStunnedState());
        }
    }

    private IEnumerator HandleStunnedState()
    {
        // Cambiar al estado Stunned
        currentState = GhostState.Stunned;

        // Esperar el tiempo de aturdimiento
        yield return new WaitForSeconds(stunnedTime);

       
        if (Vector2.Distance(transform.position, playerTransform.position) < shootRange)
        {
            currentState = GhostState.MovingToDistance;
        }
        else
        {
            currentState = GhostState.Idle;
        }
    }


}
