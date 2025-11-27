using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private bool isPaused;


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartingScene")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPaused)
            {
                Exit();
            }
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FINAL_SCENE");
        AudioListener.pause = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        isPaused = true;   
    }

    public void Resume()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPaused = false;  
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartingScene");
        AudioListener.pause = false;  
    }
}
