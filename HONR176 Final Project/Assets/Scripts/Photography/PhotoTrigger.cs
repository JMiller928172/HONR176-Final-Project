using UnityEngine;

public class PhotoTrigger : MonoBehaviour
{
    public int maxCount, currentCount, photoCount;
    public KeyCode key;

    public bool photoTaken = false;

    void Update()
    {
        if (Input.GetKeyDown(key) && !photoTaken)
        {            
            TakePhoto(currentCount);
        }
    }

    void OnTriggerEnter(Collider col){
        currentCount++;

        if(currentCount > maxCount)
            maxCount = currentCount;
    }

    void OnTriggerExit(Collider col){
        currentCount--;
    }

    void TakePhoto(int count){
        photoCount = maxCount;
        photoTaken = true;
    }
}