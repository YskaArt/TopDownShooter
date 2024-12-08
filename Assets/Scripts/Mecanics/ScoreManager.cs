using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton

    public TextMeshProUGUI scoreText;
    private int score;

    void Awake()
    {
        // Configurar el singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que el objeto se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
        }
    }

    void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        }
        UpdateScoreText();
    }

    // Método para sumar puntos al recolectar un objeto
    public void AddScoreFromObject(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Método para sumar puntos al eliminar un enemigo
    public void AddScoreFromEnemy(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Actualizar el texto del puntaje
    private void UpdateScoreText()
    {
        if (scoreText != null) 
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public int GetScore()
    {
        return score; 
    }
}
