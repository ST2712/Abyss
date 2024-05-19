using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject mainCamera;

    void Awake(){
        pauseMenuUI.SetActive(false);
    }

    void Start()
    {

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        mainCamera.GetComponent<AudioSource>().UnPause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        mainCamera.GetComponent<AudioSource>().Pause();
    }

    public void MainMenu(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
