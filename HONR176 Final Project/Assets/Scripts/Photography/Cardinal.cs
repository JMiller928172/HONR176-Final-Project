using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Cardinal
{
    public float minTime, maxTime;
    public float spawnTime;
    public bool isSpawned;

    void Awake(){
        spawnTime = Random.Range(minTime, maxTime);
    }
}