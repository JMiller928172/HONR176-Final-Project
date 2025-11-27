using UnityEngine;
using TMPro;

public class CookingManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject winMenu;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTimeText;
    public TextMeshProUGUI bestTimeText;

    [Header("Request Settings")]
    public RequestMachine requestMachine;

    private float timer;
    private bool gameRunning = true;

    void Start()
    {
        winMenu.SetActive(false);

        timer = 0f;
    }

    void Update()
    {
        if (!gameRunning) return;

        timer += Time.deltaTime;
        DisplayTime(timer);

        if (requestMachine != null && requestMachine.requestsCompleted)
        {
            GameOver();
        }
    }

    void DisplayTime(float time)
    {
        timerText.SetText(time.ToString("F2"));
    }

    void GameOver()
    {
        gameRunning = false;
        winMenu.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("CookHighScore", float.MaxValue);
        if (timer < bestTime)
        {
            PlayerPrefs.SetFloat("CookHighScore", timer);
            PlayerPrefs.Save();
        }

        finalTimeText.SetText("Your Time: " + timer.ToString("F3") + " seconds.");
        bestTimeText.SetText("Best Time: " + PlayerPrefs.GetFloat("CookHighScore").ToString("F3") + " seconds.");
    }
}