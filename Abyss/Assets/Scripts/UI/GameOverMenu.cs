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
        Debug.Log("Game Over");
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
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
