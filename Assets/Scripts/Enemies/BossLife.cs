using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossLife : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    public Slider healthBar;
    
    public GameObject Boss;
    
    public SoundDeath agony;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }
    private IEnumerator Death()
    {
        agony.Agony();
        FindObjectOfType<ScoreManager>().AddScoreFromEnemy(points);
        Boss.SetActive(false);
        yield return new WaitForSecondsRealtime(8f);
        
        
        

    }
}
