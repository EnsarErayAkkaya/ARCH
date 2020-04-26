using UnityEngine;
using UnityEngine.UI;

public class AdRemovedButtonController : MonoBehaviour
{
    void Start()
    {
        if(SaveAndLoadGameData.instance.savedData.isAdsRemoved)
        {
            GetComponent<Button>().interactable = false;
        }
        else{
            GetComponent<Button>().interactable = true;
        }
    }
}
