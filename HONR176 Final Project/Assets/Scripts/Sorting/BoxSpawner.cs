using UnityEngine;
using System.Collections.Generic;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    public RectTransform parentCanvas;
    public int totalBoxes = 10;
    public float startY = -50f;

    private int spawnedCount = 0;
    private List<GameObject> activeBoxes = new List<GameObject>();

    void Start()
    {
        SpawnNextBox();
    }

    void Update()
    {
        if (AllBoxesInSlots() && spawnedCount < totalBoxes)
        {
            SpawnNextBox();
        }
    }

    void SpawnNextBox()
    {
        GameObject newBox = Instantiate(boxPrefab, parentCanvas);

        RectTransform rt = newBox.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, startY);

        activeBoxes.Add(newBox);
        spawnedCount++;
    }

    bool AllBoxesInSlots()
    {
        foreach (GameObject box in activeBoxes)
        {
            if (box == null) continue; 

            if (box.transform.parent.GetComponent<Slot>() == null)
            {
                return false; 
            }
        }
        return true;
    }
}