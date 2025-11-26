using UnityEngine;
using TMPro;

public class ProbabilityDisplay : MonoBehaviour
{
    public TextMeshProUGUI probabilityText; 

    private ShadowSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<ShadowSpawner>();

        if (spawner == null)
        {
            Debug.LogError("No ShadowSpawner found in scene!");
        }
    }

    void Update()
    {
        if (spawner == null || probabilityText == null) return;

        probabilityText.text =
            $"Fish Probability: {(spawner.fishProbability * 100f):F1}%\n" +
            $"Shark Probability: {(spawner.sharkProbability * 100f):F1}%\n\n" +
            $"Fish Colors:\n" +
            $"• Orange: {(spawner.fishColorProbabilities["orange"] * 100f):F1}%\n" +
            $"• Grey: {(spawner.fishColorProbabilities["grey"] * 100f):F1}%\n" +
            $"• Green: {(spawner.fishColorProbabilities["green"] * 100f):F1}%\n\n" +
            $"Shark Colors:\n" +
            $"• Orange: {(spawner.sharkColorProbabilities["orange"] * 100f):F1}%\n" +
            $"• Grey: {(spawner.sharkColorProbabilities["grey"] * 100f):F1}%\n" +
            $"• Green: {(spawner.sharkColorProbabilities["green"] * 100f):F1}%";

    }
}