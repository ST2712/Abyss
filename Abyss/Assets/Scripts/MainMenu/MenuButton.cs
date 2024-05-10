using System.Collections;
using System.Collections.Generic;
using GameJolt.API;
using GameJolt.UI;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    bool isSignedIn;
    void Start()
    {
        GameJoltUI.Instance.ShowSignIn();
        isSignedIn = GameJoltAPI.Instance.CurrentUser != null;
    }

    void Update()
    {
        if(GameJoltAPI.Instance.CurrentUser != null)
        {
            isSignedIn = true;
        }
    }

    void onGUI()
    {
        if(isSignedIn)
        {
            if(GUI.Button(new Rect(10, 10, 100, 30), "Logout"))
            {
                GameJoltAPI.Instance.CurrentUser.SignOut();
                isSignedIn = false;
            }
        }
    }
}
