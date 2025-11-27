using UnityEngine;
using TMPro;

public class RequestMachine : MonoBehaviour
{
    [Header("Request Settings")]
    public int totalRequests = 8;
    public TextMeshProUGUI requestText;

    public bool requestsCompleted;

    private string currentRequest;
    private int requestsMade = 0;

    void Start()
    {
        GenerateNewRequest();

        requestsCompleted = false;
    }

    void GenerateNewRequest()
    {
        if (requestsMade >= totalRequests)
        {
            requestsCompleted = true;
            if (requestText != null)
                requestText.text = "All requests completed!";
            return;
        }
            

        currentRequest = GetWeightedIngredient();
        requestsMade++;

        if (requestText != null)
            requestText.text = "Request " + requestsMade + ": " + currentRequest;
    }

    string GetWeightedIngredient()
    {
        // Probabilities: Pepperoni 30%, Cheese 23%, Peppers 22%, Mushrooms 15%, Anchovies 10%
        float roll = Random.Range(0f, 100f);

        if (roll < 30f) return "pepperoni";
        else if (roll < 53f) return "cheese";       
        else if (roll < 75f) return "peppers";      
        else if (roll < 90f) return "mushrooms";    
        else return "anchovies";                    
    }

    void OnTriggerEnter(Collider other)
    {
        Ingredient box = other.GetComponent<Ingredient>();
        if (box != null)
        {
            if (box.ingredientName == currentRequest)
            {
                Debug.Log("Correct ingredient delivered: " + box.ingredientName);
                GenerateNewRequest();
            }
            else
            {
                Debug.Log("Wrong ingredient! Delivered: " + box.ingredientName);
            }
        }
    }
}