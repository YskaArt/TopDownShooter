using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void Play()
    {
        SceneManager.LoadScene("Introduccion");
    }
    public void QuitGame()
    {
        Debug.Log("Salir del juego...");

        Application.Quit();
    }
}
