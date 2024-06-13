using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    #region Singleton
    public static HUDManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject hudManager;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject pause;

    //============================= UI Manager ====================================
    // Start is called before the first frame update
    private void Start()
    {
        if (hudManager != null)
        {
            ShowHud();
        }
    }

    public void ShowHud()
    {
        hudManager.SetActive(true);
        this.GetComponent<PauseMenu>().Resume();
        win.SetActive(false);
        gameOver.SetActive(false);
        settings.SetActive(false);
        pause.SetActive(false);
    }

    public void ShowWin()
    {
        hudManager.SetActive(true);
        win.SetActive(true);
        gameOver.SetActive(false);
        settings.SetActive(false);
        pause.SetActive(false);
    }

    public void ShowGameOver()
    {
        hudManager.SetActive(true);
        win.SetActive(false);
        gameOver.SetActive(true);
        settings.SetActive(false);
        pause.SetActive(false);
    }

    public void ShowPause()
    {
        hudManager.SetActive(true);
        win.SetActive(false);
        gameOver.SetActive(true);
        settings.SetActive(false);
        pause.SetActive(true);
    }

    public void ShowSettings()
    {
        hudManager.SetActive(true);
        win.SetActive(false);
        gameOver.SetActive(false);
        settings.SetActive(!settings.activeSelf);
        pause.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
