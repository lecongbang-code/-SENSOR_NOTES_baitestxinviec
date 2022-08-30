using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public int targetFps = 30;
    public TMPro.TextMeshProUGUI fpsText;

    float fps;

    void Awake()
    {
        Application.targetFrameRate = targetFps;
    }

    void Start()
    {
        StartCoroutine(RecalculateFps());
    }

    void Update()
    {
        fpsText.text = "FPS: " + string.Format("{0:0}", fps);
    }

    IEnumerator RecalculateFps()
    {
        while (true)
        {
            fps = 1 / Time.deltaTime;
            yield return new WaitForSeconds(1);
        }
    }  
}
