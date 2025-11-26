using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    public List<GameObject> shadowPrefabs;   // Prefabs indexed by ShadowType enum
    public List<Shadow> shadows;             // Shadow data objects

    // Probability values
    public float fishProbability;
    public float sharkProbability;

    public Dictionary<string, float> fishColorProbabilities = new Dictionary<string, float>();
    public Dictionary<string, float> sharkColorProbabilities = new Dictionary<string, float>();

    public int shadowCount = 12;         // Number of shadows to spawn
    public float minDistance = 50f;      // Minimum distance between shadows (UI units)

    private List<Vector2> usedPositions = new List<Vector2>();
    private RectTransform rectTransform; // The rect area we spawn within

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        GenerateProbabilities();
        PrintProbabilities();
        SpawnShadows();
    }

    void GenerateProbabilities()
    {
        // Random fish vs shark probability
        fishProbability = Random.Range(0.3f, 0.7f);
        sharkProbability = 1f - fishProbability;

        // Random color probabilities for fish (normalized)
        float fishOrange = Random.Range(0.2f, 0.5f);
        float fishGrey = Random.Range(0.2f, 0.5f);
        float fishGreen = Mathf.Clamp01(1f - (fishOrange + fishGrey));

        fishColorProbabilities["orange"] = fishOrange;
        fishColorProbabilities["grey"] = fishGrey;
        fishColorProbabilities["green"] = fishGreen;

        // Random color probabilities for sharks (normalized)
        float sharkOrange = Random.Range(0.2f, 0.5f);
        float sharkGrey = Random.Range(0.2f, 0.5f);
        float sharkGreen = Mathf.Clamp01(1f - (sharkOrange + sharkGrey));

        sharkColorProbabilities["orange"] = sharkOrange;
        sharkColorProbabilities["grey"] = sharkGrey;
        sharkColorProbabilities["green"] = sharkGreen;
    }

    void PrintProbabilities()
    {
        Debug.Log($"Fish Probability: {fishProbability * 100f}%");
        Debug.Log($"Shark Probability: {sharkProbability * 100f}%");

        Debug.Log($"Fish Color Probabilities -> Orange: {fishColorProbabilities["orange"] * 100f}%, Grey: {fishColorProbabilities["grey"] * 100f}%, Green: {fishColorProbabilities["green"] * 100f}%");
        Debug.Log($"Shark Color Probabilities -> Orange: {sharkColorProbabilities["orange"] * 100f}%, Grey: {sharkColorProbabilities["grey"] * 100f}%, Green: {sharkColorProbabilities["green"] * 100f}%");
    }

    void SpawnShadows()
    {
        int spawned = 0;
        int attempts = 0;

        // Get rect bounds
        float halfWidth = rectTransform.rect.width / 2f;
        float halfHeight = rectTransform.rect.height / 2f;

        while (spawned < shadowCount && attempts < shadowCount * 20)
        {
            attempts++;

            // Random anchored position inside rect
            Vector2 randomPos = new Vector2(
                Random.Range(-halfWidth, halfWidth),
                Random.Range(-halfHeight, halfHeight)
            );

            if (!IsPositionValid(randomPos)) continue;

            // Decide fish or shark
            bool isFish = Random.value < fishProbability;

            // Decide color
            string color = GetRandomColor(isFish);

            // Pick prefab based on ShadowType enum
            ShadowType type = GetShadowType(isFish, color);
            GameObject prefab = shadowPrefabs[(int)type];

            // Instantiate as child of canvas/panel
            GameObject shadowObj = Instantiate(prefab, rectTransform);
            shadowObj.GetComponent<RectTransform>().anchoredPosition = randomPos;
            shadowObj.SetActive(true);

            usedPositions.Add(randomPos);
            spawned++;
        }
    }

    bool IsPositionValid(Vector2 pos)
    {
        foreach (Vector2 used in usedPositions)
        {
            if (Vector2.Distance(used, pos) < minDistance)
                return false;
        }
        return true;
    }

    string GetRandomColor(bool isFish)
    {
        float roll = Random.value;
        Dictionary<string, float> probs = isFish ? fishColorProbabilities : sharkColorProbabilities;

        if (roll < probs["orange"]) return "orange";
        else if (roll < probs["orange"] + probs["grey"]) return "grey";
        else return "green";
    }

    ShadowType GetShadowType(bool isFish, string color)
    {
        if (isFish)
        {
            if (color == "orange") return ShadowType.orangeFish;
            if (color == "grey") return ShadowType.greyFish;
            return ShadowType.greenFish;
        }
        else
        {
            if (color == "orange") return ShadowType.orangeShark;
            if (color == "grey") return ShadowType.greyShark;
            return ShadowType.greenShark;
        }
    }
}