using System;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static bool _gameIsPaused;
    public GameObject _pauseMenuUI;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
                Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        _gameIsPaused = false;
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        _gameIsPaused = true;
    }

    public void SaveAndQuit()
    {
        throw new NotImplementedException();
    }
}
