using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;
    
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            transform.position = startPosition;
        }
    }
}
