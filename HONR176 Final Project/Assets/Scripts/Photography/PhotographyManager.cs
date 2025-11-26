using UnityEngine;
using TMPro;

public class PhotographyManager : MonoBehaviour
{
    public PhotoTrigger trig;
    public float maxTime;
    bool levelRunning = true;

    float timer;

    public GameObject timeMenu;
    public TextMeshProUGUI timeText;

    public GameObject helpMenu;
    public TextMeshProUGUI helpText;

    public GameObject resultsMenu;
    public TextMeshProUGUI highScoreText, scoreText, maxScoreText;

    void Start()
    {
        helpText.SetText("Press " + trig.key.ToString() + " to take a photo!");
        helpMenu.SetActive(true);
        timeMenu.SetActive(true);
        resultsMenu.SetActive(false);

        timer = 0f;
    }

    void Update()
    {
        if (!levelRunning) return;

        timer += Time.deltaTime;

        float remaining = Mathf.Max(0, maxTime - timer);
        
        timeText.SetText(Mathf.CeilToInt(remaining).ToString());

        if(trig.photoTaken){
            helpMenu.SetActive(false);
        }

        if (timer >= maxTime)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        levelRunning = false;
        resultsMenu.SetActive(true);
        timeMenu.SetActive(false);
        helpMenu.SetActive(false);

        int highScore = PlayerPrefs.GetInt("PhotoHighScore", 0);

        if (trig.photoCount > highScore)
        {
            highScore = trig.photoCount;
            PlayerPrefs.SetInt("PhotoHighScore", highScore);
            PlayerPrefs.Save();
        }

        scoreText.SetText("You took a photo with " + trig.photoCount.ToString() + " birds!");
        maxScoreText.SetText("The most amount of birds in frame was " + trig.maxCount + ".");
        highScoreText.SetText("High Score: " + highScore.ToString());
    }
}