using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    int score;

    public AudioSource hitAudio;

    void Start()
    {
        Instance = this;
        score = 0;
    }

    public void Hit()
    {
        score += 1;
        scoreText.text = score.ToString();
        hitAudio.Play();
    }

    public static void TextSongDelayInSeconds(float timeDelayInSeconds)
    {
        if (timeDelayInSeconds <= 0) timeDelayInSeconds = 0;
        Instance.scoreText.text = Math.Round(timeDelayInSeconds, 1).ToString();
    }
}