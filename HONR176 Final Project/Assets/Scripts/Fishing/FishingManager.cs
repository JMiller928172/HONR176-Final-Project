using UnityEngine;
using TMPro;

public class FishingManager : MonoBehaviour
{
    public int fishCaught;
    public int fishEscaped;
    public int sharksCaged;
    public int sharksEscaped;

    public GameObject resultsMenu, switchMenu;
    public Canvas gameCanvas;
    public TextMeshProUGUI fishCaughtText;
    public TextMeshProUGUI fishEscapedText;
    public TextMeshProUGUI sharksCagedText;
    public TextMeshProUGUI sharksEscapedText;
    public TextMeshProUGUI totalCatchesText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        resultsMenu.SetActive(false);
        switchMenu.SetActive(true);
    }

    void Update()
    {
        Shadow[] activeShadows = FindObjectsOfType<Shadow>();

        if (activeShadows.Length == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        resultsMenu.SetActive(true);
        switchMenu.SetActive(false);

        int totalCatches = fishCaught + sharksCaged;
        int highScore = PlayerPrefs.GetInt("FishingHighScore", 0);

        if (totalCatches > highScore)
        {
            highScore = totalCatches;
            PlayerPrefs.SetInt("FishingHighScore", highScore);
            PlayerPrefs.Save();
        }

        fishCaughtText.SetText("Fish Caught: " + fishCaught);
        fishEscapedText.SetText("Fish Escaped: " + fishEscaped);
        sharksCagedText.SetText("Sharks Caged: " + sharksCaged);
        sharksEscapedText.SetText("Sharks Escaped: " + sharksEscaped);
        totalCatchesText.SetText("Total Catches: " + totalCatches);
        highScoreText.SetText("High Score: " + highScore);
    }
}