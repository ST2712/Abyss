using System.Collections;
using System.Collections.Generic;
using GameJolt.API;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(50, 50, 100, 30), "Trophy"))
        {
            GameJolt.API.Trophies.Unlock(232668, (bool success) => {
                if (success)
                {
                    Debug.Log("Trophy unlocked!");
                }
                else
                {
                    Debug.Log("Trophy not unlocked.");
                }
            });
        }
    }
}
