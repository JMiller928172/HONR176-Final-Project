using UnityEngine;

public class SwimEffect : MonoBehaviour
{
    public Vector2 direction = Vector2.up;
    public float speed = 100f;
    public float lifetime = 3f;

    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        rect.anchoredPosition += direction * speed * Time.deltaTime;
    }
}
