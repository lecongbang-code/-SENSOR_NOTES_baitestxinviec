using System.Collections;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;

    public int targetFps = 30;
    public TMPro.TextMeshProUGUI fpsText;
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

    IEnumerator RecalculateFps()
    {
        while (true)
        {
            fps = 1 / Time.deltaTime;
            fpsText.text = "FPS: " + string.Format("{0:0}", fps);
            yield return new WaitForSeconds(1);
        }
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }

    public static void ActiveUIQuitGame()
    {
        Instance.quitGamePanel.SetActive(true);
    }

    public static void FinishGame()
    {
        finishGame = true;
        SongManager.Instance.StopSong();
        ActiveUIQuitGame();
    }
}
