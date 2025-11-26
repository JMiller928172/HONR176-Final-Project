using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SortingManager : MonoBehaviour
{
    public Slot[] slots;

    public GameObject winMenu;
    public TextMeshProUGUI timerText, bestTimeText;

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
        timerText.SetText("Time: " + Mathf.FloorToInt(timer).ToString());

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


    void GameOver()
    {
        gameRunning = false;

        Debug.Log("You beat the game!");

        winMenu.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (timer < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", timer);
            PlayerPrefs.Save();
        }

        bestTimeText.SetText("Best Time: " + Mathf.FloorToInt(PlayerPrefs.GetFloat("BestTime")).ToString());
    }

    public void CallRefresh(){
        foreach(Slot slot in slots){
            slot.Refresh();
        }
    }
}