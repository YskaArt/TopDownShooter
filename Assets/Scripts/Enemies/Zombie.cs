using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float speed = 2f;
    public float chaseRange = 10f;
    public PlayerHealth playerhealth;
    private float stunnedTime = 2f;
    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private Transform playerTransform;
    public enum ZombieState
    {
        Idle, ChasingPlayer, Stunned
    }
    private ZombieState currentState;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player != null)
        {
            
            playerHealth = player.GetComponent<PlayerHealth>();

            
            playerTransform = player.transform;
        }
        currentState = ZombieState.Idle;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case ZombieState.Idle:
                animator.Play("idle");
                if (Vector2.Distance(transform.position, playerTransform.position) < chaseRange)
                {
                    currentState = ZombieState.ChasingPlayer;
                }
                break;

            case ZombieState.ChasingPlayer:
                animator.Play("Walk");

                // Calcula la dirección hacia el jugador
                Vector2 direction = (playerTransform.position - transform.position).normalized;

                // Movimiento hacia el jugador
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

                // Invertir la escala en el eje X según la dirección
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(2, 2, 2);  // Mira hacia la derecha
                    spriteRenderer.flipX = false;
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(2, 2, 2);  // Mira hacia la izquierda
                    spriteRenderer.flipX = true;
                }
                break;
           
            case ZombieState.Stunned:
                animator.Play("idle");
                // No hace nada, solo espera
                rb.velocity = Vector2.zero;
                break;
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
        currentState = ZombieState.Stunned;

        // Esperar el tiempo de aturdimiento
        yield return new WaitForSeconds(stunnedTime);

        // Volver al estado Idle o ChasingPlayer
        if (Vector2.Distance(transform.position, playerTransform.position) < chaseRange)
        {
            currentState = ZombieState.ChasingPlayer;
        }
        else
        {
            currentState = ZombieState.Idle;
        }
    }
}

    
    

