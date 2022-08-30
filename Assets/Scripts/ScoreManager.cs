using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    static int score;

    void Start()
    {
        Instance = this;
        score = 0;
    }

    public static void Hit()
    {
        score += 1;
    }
  
    private void Update()
    {
        scoreText.text = score.ToString();
    }
}