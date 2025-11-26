using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int number;
    public TextMeshProUGUI displayText;
    public Image boxImage;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    [HideInInspector]
    public Transform originalParent;
    [HideInInspector] 
    public Color originalColor;

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        new Color(0.5f, 0f, 0.5f), // purple
        new Color(1f, 0.5f, 0f),   // orange
        new Color(0f, 0.5f, 0.5f)  // teal
    };

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        Color randomColor = colors[Random.Range(0, colors.Length)];
        boxImage.color = randomColor;
        originalColor = randomColor;

        number = Random.Range(1, 1000);

        displayText.SetText(number.ToString("D3"));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); 
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == transform.root)
        {
            boxImage.color = originalColor;
        }

    }
}