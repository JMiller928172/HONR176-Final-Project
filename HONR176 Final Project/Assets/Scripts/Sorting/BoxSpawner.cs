using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab; 
    public RectTransform parentCanvas; 
    public float spacing = 100f;   

    void Start()
    {
        SpawnBoxes();
    }

    void SpawnBoxes()
    {
        for (int i = 0; i < 10; i++)
        {
            
            GameObject newBox = Instantiate(boxPrefab, parentCanvas);

            RectTransform rt = newBox.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i * spacing, -50f); 
        }
    }
}