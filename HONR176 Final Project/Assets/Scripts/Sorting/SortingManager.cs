using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SortingManager : MonoBehaviour
{
    public GameObject boxPrefab;
    public Transform[] slots; // 10 slot positions
    public TextMeshProUGUI timerText, bestTimeText, recommendationText;

    private Queue<int> numbers = new Queue<int>();
    private float timer;
    private bool gameRunning = true;

    void Start()
    {
        // Generate 10 random numbers
        List<int> nums = new List<int>();
        for (int i = 0; i < 10; i++)
            nums.Add(Random.Range(1, 1000));

        // Shuffle
        for (int i = 0; i < nums.Count; i++)
        {
            int temp = nums[i];
            int rand = Random.Range(i, nums.Count);
            nums[i] = nums[rand];
            nums[rand] = temp;
        }

        // Queue them
        foreach (int n in nums)
            numbers.Enqueue(n);

        recommendationText.SetText("Try Bubble Sort or Insertion Sort!");
    }

    void Update()
    {
        if (!gameRunning) return;

        timer += Time.deltaTime;
        timerText.SetText("Time: " + Mathf.FloorToInt(timer).ToString());
    }

    public void PlaceBox(int slotIndex)
    {
        if (numbers.Count == 0) return;

        int nextNum = numbers.Dequeue();
        GameObject newBox = Instantiate(boxPrefab, slots[slotIndex].position, Quaternion.identity);
        newBox.GetComponentInChildren<TextMeshProUGUI>().SetText(nextNum.ToString("D3"));

        if (numbers.Count == 0)
            EndGame();
    }

    void EndGame()
    {
        gameRunning = false;

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (timer < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", timer);
            PlayerPrefs.Save();
        }

        bestTimeText.SetText("Best Time: " + Mathf.FloorToInt(PlayerPrefs.GetFloat("BestTime")).ToString());
    }
}