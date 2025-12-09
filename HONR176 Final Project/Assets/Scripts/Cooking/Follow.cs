using UnityEngine;

public class Follow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public Vector3 offset;        

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}