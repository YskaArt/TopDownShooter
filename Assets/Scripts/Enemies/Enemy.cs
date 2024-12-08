using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private int maxHealth; 
    private int currentHealth;
    public EnemyDrop enemyDrop;
    public Slider healthBar;
    private AudioSource effect;
    [SerializeField] private AudioClip DeathSound;


    void Start()
    {
        enemyDrop = GetComponent<EnemyDrop>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        effect = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (enemyDrop != null)
        {
            enemyDrop.TryDropItem();
        }
        
        StartCoroutine(Death());

       
    }
    private IEnumerator Death()
    {

        effect.PlayOneShot(DeathSound);
        FindObjectOfType<ScoreManager>().AddScoreFromEnemy(points);
        yield return new WaitForSecondsRealtime(0.4f);

        Destroy(gameObject);

    }
}
