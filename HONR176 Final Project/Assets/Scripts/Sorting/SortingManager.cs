using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SortingManager : MonoBehaviour
{
    public Slot[] slots;

    public GameObject winMenu;
    public TextMeshProUGUI timerText, finalTimeText, bestTimeText;

    private float timer;
    private bool gameRunning = true;

    void Start(){
        winMenu.SetActive(false);

        foreach(Slot slot in slots){
            slot.sm = this;
        }
    }

    void Update()
    {
        if (!gameRunning) return;

        timer += Time.deltaTime;
        DisplayTime(timer);

        if(AllSlotsFilled() && BoxesAreSorted()){
            GameOver();
        }
    }

    bool AllSlotsFilled()
    {
        foreach (Slot slot in slots)
        {
            if (!slot.hasBox) return false;
        }
        return true;
    }

    bool BoxesAreSorted()
    {
        int prev = int.MinValue;
        foreach (Slot slot in slots)
        {
            if (!slot.hasBox) return false;

            int current = slot.GetNumber();
            if (current < prev && current != -1) return false;
            prev = current;
        }
        return true;
    }

    void DisplayTime(float timer){
        timerText.SetText(timer.ToString("F2"));
    }


    void GameOver()
    {
        gameRunning = false;

        winMenu.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("SortHighScore", float.MaxValue);
        if (timer < bestTime)
        {
            PlayerPrefs.SetFloat("SortHighScore", timer);
            PlayerPrefs.Save();
        }

        finalTimeText.SetText("Your Time: " + timer.ToString("F2") + " seconds.");

        bestTimeText.SetText("Best Time: " + PlayerPrefs.GetFloat("SortHighScore").ToString("F2") + " seconds.");
    }


    public void CallRefresh(){
        foreach(Slot slot in slots){
            slot.Refresh();
        }
    }
}