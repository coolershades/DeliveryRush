using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject _pauseMenuUI;
    public static bool _gameIsPaused;

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
            else Activate();
        }
    }

    public void Activate()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        _gameIsPaused = true;
    }

    public void SaveAndQuit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        _gameIsPaused = false;
    }
}
