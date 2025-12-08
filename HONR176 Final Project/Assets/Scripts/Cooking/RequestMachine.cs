using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequestMachine : MonoBehaviour
{
    [Header("Request Settings")]
    public int totalRequests = 8;
    public TextMeshProUGUI requestText;
    public Sprite pepperoni, cheese, peppers, anchovies, mushrooms;
    public Image imageHolder;

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
            requestText.text = "Request " + requestsMade;

        if (imageHolder != null)
        {
            switch (currentRequest)
            {
                case "pepperoni":
                    imageHolder.sprite = pepperoni;
                    break;
                case "cheese":
                    imageHolder.sprite = cheese;
                    break;
                case "peppers":
                    imageHolder.sprite = peppers;
                    break;
                case "mushrooms":
                    imageHolder.sprite = mushrooms;
                    break;
                case "anchovies":
                    imageHolder.sprite = anchovies;
                    break;
                default:
                    imageHolder.sprite = null;
                    break;
            }
        }
    }

    string GetWeightedIngredient()
    {
        // Probabilities: Pepperoni 28%, Cheese 24%, Peppers 20%, Mushrooms 15%, Anchovies 13%
        float roll = Random.Range(0f, 100f);

        if (roll < 28f) return "pepperoni";
        else if (roll < 52f) return "cheese";       
        else if (roll < 72f) return "peppers";      
        else if (roll < 87f) return "mushrooms";    
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