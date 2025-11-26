using UnityEngine;

[System.Serializable]

public class Shadow : MonoBehaviour
{
    public ShadowType shadowType;

    public bool isFish;

    HookController hc;

    void Awake(){
        hc = GameObject.FindWithTag("HookController").GetComponent<HookController>();
    }

    public void Fished(){
        bool hook = hc.hookSelected;

        if(isFish && hook)
            Debug.Log("You hooked a fish!");
        else if (!isFish && hook)
            Debug.Log("You hooked a shark :(");
        else if (isFish && !hook)
            Debug.Log("You caged a fish :(");
        else
            Debug.Log("You caged a shark!");

        Destroy(this.gameObject);
    }
}

public enum ShadowType{
    orangeFish,
    orangeShark,
    greenFish,
    greenShark,
    greyFish,
    greyShark
}
