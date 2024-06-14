using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUICanvas;
    public GameObject _PmUi;

    public GameObject _SmUi;

    bool settingsActive;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingsActive)
            {
                Settings();
            }
            else if (GameIsPaused)
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
        pauseMenuUICanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUICanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
    }

    public void Settings()
    {
        _PmUi.SetActive(!_PmUi.activeSelf);
        _SmUi.SetActive(!_SmUi.activeSelf);

        if(_SmUi.activeSelf == true)
        {
            settingsActive = true;
        } else
        {
            settingsActive = false;
        }
    }
}
