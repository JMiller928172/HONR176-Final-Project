using UnityEngine;

[System.Serializable]
public class Shadow : MonoBehaviour
{
    public ShadowType shadowType;
    public bool isFish;

    public GameObject fishPrefab;
    public GameObject sharkPrefab;

    HookController hc;
    FishingManager fm;

    void Awake()
    {
        hc = GameObject.FindWithTag("HookController").GetComponent<HookController>();
        fm = GameObject.FindWithTag("HookController").GetComponent<FishingManager>();
    }

    public void Fished()
    {
        bool hook = hc.hookSelected;
        GameObject prefabToSpawn = isFish ? fishPrefab : sharkPrefab;

        // Spawn inside the canvas
        GameObject spawned = Instantiate(prefabToSpawn, fm.gameCanvas.transform);

        // Copy the anchored position of the shadow
        RectTransform shadowRect = GetComponent<RectTransform>();
        RectTransform spawnedRect = spawned.GetComponent<RectTransform>();

        spawnedRect.anchoredPosition = shadowRect.anchoredPosition;


        SwimEffect swim = spawned.GetComponent<SwimEffect>();

        if (isFish && hook)
        {
            fm.fishCaught++;
            Debug.Log("You caught a fish!");
            swim.direction = Vector2.up; // rise
        }
        else if (!isFish && hook)
        {
            fm.sharksEscaped++;
            Debug.Log("You hooked a shark :(");
            swim.direction = Vector2.right; // swim away
        }
        else if (isFish && !hook)
        {
            fm.fishEscaped++;
            Debug.Log("You caged a fish :(");
            swim.direction = Vector2.left; // wrong guess
        }
        else
        {
            fm.sharksCaged++;
            Debug.Log("You caged a shark!");
            swim.direction = Vector2.up; // rise
        }

        Destroy(this.gameObject);
    }

}

public enum ShadowType{
    orangeFish,
    orangeShark,
    greenFish,
    greenShark,
    greyFish,
    greyShark
}