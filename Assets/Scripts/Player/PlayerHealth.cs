using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int damageFromEnemy;  // Daño que recibe el jugador de los enemigos
    [SerializeField] private int damageFromFireball; // Daño que recibe el jugador de las fireballs
    public Slider healthBar; // Barra de salud del jugador
    [SerializeField] private int maxHealth; // Salud máxima del jugador
    private int currentHealth; // Salud actual del jugador

    public GameObject gameOverScreen;
    public GameObject weapon;
    public PauseMenu pauseMenu;
    void Start()
    {
        gameOverScreen.SetActive(false);
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            TakeDamage(damageFromEnemy);
        }
        else if (other.CompareTag("Fireball"))
        {
            
            TakeDamage(damageFromFireball);
            Destroy(other.gameObject);
        }
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            // Cuando la vida llega a 0, el juego termina
            gameOver();
        }
    }

    
    void gameOver()
    {
        Time.timeScale = 0f;
        
        gameOverScreen.SetActive(true);
        weapon.SetActive(false);
        pauseMenu.GameOver();



    }

    public bool IsAtMaxHealth()
    {
        return currentHealth >= maxHealth;
    }

   
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Asegura que no pase del máximo
        healthBar.value = currentHealth; // Actualiza la barra de salud
        
    }
    
  
}
