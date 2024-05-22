using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public static PointsController instance;
    [SerializeField] public int points;

    private void Awake()
    {
        if (PointsController.instance == null)
        {
            PointsController.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void addPoints(int points)
    {
        this.points += points;
    }
}