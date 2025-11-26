using UnityEngine;
using TMPro;

public class HookController : MonoBehaviour
{
    public bool hookSelected;

    public KeyCode switchKey;

    public TextMeshProUGUI switchText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        hookSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey))
            SwitchHook();
    }

    public void SwitchHook(){
        hookSelected = !hookSelected;

        if(hookSelected)
            switchText.SetText("Switch to Cage");
        else
            switchText.SetText("Switch to Hook");
    }
}
