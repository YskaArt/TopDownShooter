using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string normalScene; // Nombre de la escena para puntajes menores a 1000
    [SerializeField] private string highScoreScene; // Nombre de la escena para puntajes de 1000 o más
    public ScoreManager Score;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica si el ScoreManager existe
            
            
                int currentScore = Score.GetScore();

                // Cambia de escena según el puntaje
                if (currentScore >= 1000)
                {
                    SceneManager.LoadScene(highScoreScene);
                }
                else
                {
                    SceneManager.LoadScene(normalScene);
                }
            

        }

    }

}