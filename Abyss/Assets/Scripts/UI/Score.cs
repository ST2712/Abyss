using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    private float points;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        points = 0;
    }

    private void Update()
    {
        textMesh.text = points.ToString("0");
    }

    public void getPoints(int points){
        this.points = points;
    }
}
