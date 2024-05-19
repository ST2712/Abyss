using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneIndex;

    void Update()
    {

    }

    public void GoToAnotherScene(int sceneIndex)
    {

        if (sceneIndex != -1)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
