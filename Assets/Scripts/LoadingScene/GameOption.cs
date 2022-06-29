using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOption : Singleton<GameOption>
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;
    public GameObject quitPopup;

    void Update()
    {
        //옵션창 켜기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == true)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        if (quitPopup.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
            quitPopup.SetActive(false);
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitPopup()
    {
        quitPopup.SetActive(true);
    }

    public void QuitYes()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitNo()
    {
        quitPopup.SetActive(false);
    }
}
