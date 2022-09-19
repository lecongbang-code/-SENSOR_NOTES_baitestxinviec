using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    static int score;

    public AudioSource hitAudio;

    void Start()
    {
        Instance = this;
        score = 0;
    }

    public static void Hit()
    {
        score += 1;
        Instance.scoreText.text = score.ToString();
        Instance.hitAudio.Play();
    }

    public static void TextSongDelayInSeconds(float timeDelayInSeconds)
    {
        if (timeDelayInSeconds <= 0) timeDelayInSeconds = 0;
        Instance.scoreText.text = Math.Round(timeDelayInSeconds, 2).ToString();
    }
}