using UnityEngine;
using UnityEngine.UI; // Needed for Image

public class HookController : MonoBehaviour
{
    public bool hookSelected;

    public KeyCode switchKey;

    [Header("UI Images")]
    public GameObject hookImage;
    public GameObject cageImage;

    void Awake()
    {
        hookSelected = true;
        UpdateImages();
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
            SwitchHook();
    }

    public void SwitchHook()
    {
        hookSelected = !hookSelected;
        UpdateImages();
    }

    void UpdateImages()
    {
        if (hookImage != null) hookImage.SetActive(hookSelected);
        if (cageImage != null) cageImage.SetActive(!hookSelected);
    }
}