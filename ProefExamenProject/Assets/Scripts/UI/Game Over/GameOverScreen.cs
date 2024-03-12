using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    public Scene mainMenu;
    
    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
        screen.SetActive(false);
    }

    public void MainMenu()
    {
        string menuName = mainMenu.name;
        SceneManager.LoadScene(menuName);
        Time.timeScale = 1;
    }

    public void MenuOpen()
    {
        Time.timeScale = 0;
        screen.SetActive(true);
    }
}
