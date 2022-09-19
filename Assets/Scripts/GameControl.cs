using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;

    public int targetFps = 30;
    public TMPro.TextMeshProUGUI fpsText;
    public TMPro.TextMeshProUGUI playerPositon;
    public GameObject quitGamePanel;

    float fps;
    public static bool finishGame = false;

    void Awake()
    {
        Time.timeScale = 1;
        Application.targetFrameRate = targetFps;
    }

    void Start()
    {
        Instance = this;
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

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void ActiveUIQuitGame()
    {
        Instance.quitGamePanel.SetActive(true);
    }

    public static void PlayerPositon(Vector3 playerP)
    {
        Instance.playerPositon.text = playerP.ToString();
    }

    public static void FinishGame()
    {
        finishGame = true;
        ActiveUIQuitGame();
    }
}
