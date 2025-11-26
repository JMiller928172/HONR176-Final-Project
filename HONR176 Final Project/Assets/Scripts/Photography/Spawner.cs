using System.Collections;
using System.Collections.Generic; 
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject cardinalPrefab;

    public List<Cardinal> cardinals;

    [Header("Forces")]
    public float minForce = 5f;
    public float maxForce = 10f;

    [Header("Spawning Dimensions")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    void Update()
    {
        foreach (Cardinal card in cardinals)
        {
            if (!card.isSpawned && card.spawnTime <= Time.time)
            {
                SpawnCardinal(cardinalPrefab);
                card.isSpawned = true;
            }
        }
    }

    void SpawnCardinal(GameObject card)
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);
        
        GameObject instance = Instantiate(card, new Vector3(x, y, z), Quaternion.identity);

        Rigidbody rb = instance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomForce = Random.Range(minForce, maxForce);
            rb.AddForce(new Vector3 (-1, 0, 0) * randomForce, ForceMode.Impulse);
        }
    }
}
