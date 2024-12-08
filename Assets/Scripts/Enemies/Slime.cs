using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float jumpForce = 5f; // Fuerza del salto
    public float prepareTime = 1f; // Tiempo de preparación antes de saltar
    private float timer;
    private bool isJumping;
    private PlayerHealth playerHealth;
    private Transform playerTransform;
    private float stunnedTime = 2f;
    public enum SlimeState
    {
        Idle, PreparingJump, Jumping, Stunned
    }
    private SlimeState currentState;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        
        if (player != null)
        {
            
            playerHealth = player.GetComponent<PlayerHealth>();

            
            playerTransform = player.transform;
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = SlimeState.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case SlimeState.Idle:
                animator.Play("IdleSlime");
                timer += Time.deltaTime;
                if (timer >= prepareTime)
                {
                    currentState = SlimeState.PreparingJump;
                    timer = 0;
                }
                break;

            case SlimeState.PreparingJump:
                animator.Play("JumpSlime");
                timer += Time.deltaTime;
                if (timer >= prepareTime)
                {
                    currentState = SlimeState.Jumping;
                    JumpTowardsPlayer();
                    timer = 0;
                }
                break;

            case SlimeState.Jumping:
        
                if (!isJumping)  // Detecta si la animación de salto ha terminado
                {
                    currentState = SlimeState.Idle;
                    rb.velocity = Vector2.zero; // Detenemos la velocidad
                }
                break;
            case SlimeState.Stunned:
                animator.Play("IdleSlime");
                // Durante el estado Stunned, no debería moverse
                rb.velocity = Vector2.zero; // Detener el movimiento
                break;
        }
    }

    // Detecta la colisión con el jugador o el objeto con el que se golpea
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambiar a estado Stunned por unos segundos
            StartCoroutine(HandleStunnedState());
        }
    }

    private IEnumerator HandleStunnedState()
    {
        // Cambiar al estado Stunned
        currentState = SlimeState.Stunned;

        // Esperar el tiempo de aturdimiento
        yield return new WaitForSeconds(stunnedTime);

        // Volver al estado Idle o al que esté relacionado
        currentState = SlimeState.Idle;
    }

    

    void JumpTowardsPlayer()
    {
        // Calcula la dirección hacia el jugador
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Realiza el salto con la dirección calculada
        rb.velocity = new Vector2(direction.x * jumpForce, direction.y * jumpForce);

        
        isJumping = true;
        animator.Play("CompleteJump");
    }

    // Llamada cuando la animación de salto termina
    public void OnJumpAnimationFinished()
    {
        isJumping = false;
    }
   
    
}
