using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menuItems;

    void Start(){
        enableItem(0);
    }

    public void enableItem(int i){
        foreach(GameObject item in menuItems){
            item.SetActive(false);
        }
        
        menuItems[i].SetActive(true);
    }
}
