using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenuUI;
    private Health health;
    public GameObject mainCamera;

    void Start()
    {
        health = GameObject.Find("Player").GetComponent<Health>();
    }

    public void GameOver()
    {
        mainCamera.GetComponent<AudioSource>().Stop();
        gameOverMenuUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
