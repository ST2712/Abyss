using System.Collections;
using System.Collections.Generic;
using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private bool isSignedIn;
    void Start()
    {
        GameJoltUI.Instance.ShowSignIn();
        isSignedIn = GameJoltAPI.Instance.CurrentUser != null;
    }

    void Update()
    {
        if (GameJoltAPI.Instance.CurrentUser != null)
        {
            isSignedIn = true;
            GameJolt.API.Trophies.Unlock(232668, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Trophy unlocked!");
                }
                else
                {
                    Debug.Log("Trophy not unlocked.");
                }
            });
            SceneManager.LoadScene(0);
        }
    }

    void OnGUI()
    {
        if (isSignedIn)
        {
            if (GUI.Button(new Rect(10, 10, 100, 30), "Logout"))
            {
                GameJoltAPI.Instance.CurrentUser.SignOut();
                isSignedIn = false;
            }
        }
    }

    public bool IsSignedIn()
    {
        return isSignedIn;
    }
}
