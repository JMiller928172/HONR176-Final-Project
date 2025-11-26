using UnityEngine;

[System.Serializable]

public class Shadow : MonoBehaviour
{
    public ShadowType shadowType;

    public bool isFish;

    HookController hc;
    FishingManager fm;

    void Awake(){
        hc = GameObject.FindWithTag("HookController").GetComponent<HookController>();
        fm = GameObject.FindWithTag("HookController").GetComponent<FishingManager>();
    }

    public void Fished(){
        bool hook = hc.hookSelected;

        if(isFish && hook){
            fm.fishCaught++;
            Debug.Log("You caught a fish!");
        }
            
        else if (!isFish && hook){
            fm.sharksEscaped++;
            Debug.Log("You hooked a shark :(");
        }
            
        else if (isFish && !hook){
            fm.fishEscaped++;
            Debug.Log("You caged a fish :(");
        }
            
        else{
            fm.sharksCaged++;
            Debug.Log("You caged a shark!");
        }
            

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
