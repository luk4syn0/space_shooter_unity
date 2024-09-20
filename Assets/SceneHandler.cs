using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneHandler : MonoBehaviour
{
    private InputAction pauseAction;
    private float buttonTimer;

    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public Player player;

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI scoreFinal;
    private int score;
    private float timer;
    private int timeSurvived;
    public int combatPoints;
    
    private void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        
        score = 0;
        timeSurvived = 0;
        scoreUI.text = score.ToString();
        timer = 0.0f;
        
        Time.timeScale = 1;
        
    }

    private void Update()
    {
        //Obsługa menu pauzy
        if (pauseAction.IsPressed() && buttonTimer <= 0f && !gameOverScreen.activeSelf)
        {
            buttonTimer = 60f; //Ilość klatek do przeczekania (tymczasowe rozwiązanie problemu)
            if (pauseMenu.activeSelf)
            {
                ClosePauseMenu();
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
                ShowPauseMenu();
            }
            
            
        }

        if (buttonTimer > 0f)
        {
            buttonTimer -= 1;
        }
        
        
        //Check życia gracza
        if (player.currentHealth <= 0 && !pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            GameOver();
        }

        timer += Time.deltaTime;
        timeSurvived = (int)(timer % 60);
        score = timeSurvived + combatPoints;
        scoreUI.text = score.ToString();

    }
    
    public void GameOver()
    {
        scoreFinal.text = score.ToString();
        gameOverScreen.SetActive(true);
        print("GameOver");
    }

    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        print("Pause");
    }

    private void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        print("Unpause");
    }
    

    
}
