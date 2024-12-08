using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;  
    private bool isPaused = false;
    public GameObject weapon;
    private bool isGameOver = false;
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        weapon.SetActive(true);
        pauseMenuUI.SetActive(false);  
        Time.timeScale = 1f;  
        isPaused = false;
        
    }

    void Pause()
    {
       
        weapon.SetActive(false);
        pauseMenuUI.SetActive(true);  
        Time.timeScale = 0f;  
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego...");
        
        Application.Quit(); 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;  // Asegúrate de reanudar el tiempo al reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual
    }
    public void GameOver()
    {
        isGameOver = true; 

    }
}
