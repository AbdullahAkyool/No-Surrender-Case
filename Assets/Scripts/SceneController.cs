using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour        //menülerin ve sahnelerin ayarlanması
{
    public GameObject pauseMenuPanel;
    public GameObject deathMenuPanel;
    public GameObject WinMenuPanel;
    private PlayerController playerController;
    private CurrentPlayerControl currentPlayerControl;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        currentPlayerControl = FindObjectOfType<CurrentPlayerControl>();
    }
    private void Update()
    {
        if (playerController.isDeath == true)
        {
            StartCoroutine(DeathDisplay());
        }

        if (playerController.isDeath == false && currentPlayerControl.Players.Count == 1)
        {
            StartCoroutine(WinDisplay());
        }
    }
    
    IEnumerator DeathDisplay()
    {
        yield return new WaitForSeconds(4f);
        deathMenuPanel.SetActive(true);
    }
    
    IEnumerator WinDisplay()
    {
        yield return new WaitForSeconds(4f);
        WinMenuPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
