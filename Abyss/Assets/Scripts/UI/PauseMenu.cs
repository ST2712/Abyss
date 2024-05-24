using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.AI;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject mainCamera;

    private GameObject player;
    private CombatScript combatScript;

    void Awake()
    {
        pauseMenuUI.SetActive(false);
        player = GameObject.Find("Player");
        combatScript = player.GetComponent<CombatScript>();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        ChangeStateOfComponents(true);
        player.GetComponent<CombatScript>().canAttack = true;
        mainCamera.GetComponent<AudioSource>().UnPause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        ChangeStateOfComponents(false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<CombatScript>().canAttack = false;
        player.GetComponent<CombatScript>().timeNextPunch = 0.1f;
        mainCamera.GetComponent<AudioSource>().Pause();
    }

    private void ChangeStateOfComponents(bool state)
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies != null && enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<NavMeshAgent>().enabled = state;
                enemy.GetComponent<Enemy>().enabled = state;
                enemy.GetComponent<FollowObjective>().enabled = state;
                enemy.GetComponent<Animator>().enabled = state;
            }
        }

        player.GetComponent<TopDownMovement>().enabled = state;
        player.GetComponent<InventoryController>().enabled = state;
        player.GetComponent<TopDownMovement>().enabled = state;
        player.GetComponent<Animator>().enabled = state;

        gameIsPaused = !state;
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
