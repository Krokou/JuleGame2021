using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
    
    private void Pause(){
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void LoadMenu(){
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void QuitGame(){
        Debug.Log("Qitting game");
        Application.Quit();
    }
}
