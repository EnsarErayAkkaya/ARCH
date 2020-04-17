using System.Linq;
using UnityEngine;
using TMPro;
public class ProjectilePacketUIController : MonoBehaviour
{
    [SerializeField] GameObject marketPanel,highlightPrefab;
    [SerializeField]PacketType type;
    [SerializeField] string notSoldText,chooseText,cancelText;
    [SerializeField] TextMeshProUGUI sellButtonText,nameText;
    [SerializeField] Sprite normal,middle,powerful;
    ProjectileManager manager;
    bool isSold;
    void Start()
    {
        manager = FindObjectOfType<ProjectileManager>();
        nameText.text = type.ToString() + " Packet";
        CheckIsOwned();
    }
    void CheckIsOwned()
    {
        isSold = ProjectileManager.instance.ownedPackets.Any(s => s == type);

        ChangeText();
    }
    void ChangeText()
    {
        if(isSold)
        {
            //this means owned
            if(manager.choosedPacketType == type)
            {
                //already choosed
                sellButtonText.text = cancelText;
            }
            else
            {
                //Not choosed
                sellButtonText.text = chooseText;
            }
        }
        else{
            //not owned
            sellButtonText.text = notSoldText;
        }
    }

    public void OnSellButtonClick()
    {
        if(isSold == false)
        {
            //Sell with real money
            manager.OwnPacket(type);
            isSold = true;
            ChangeText();
        }
        else
        {
            if(manager.choosedPacketType == type)
            {
                manager.DeselectPacket();
                ChangeText();
            }
            else{
                manager.SelectPacket(type);
                ChangeText();
            }
        }
      
    }
    public void OnHiglightButtonClick()
    {
        GameObject obj = Instantiate(highlightPrefab);
        obj.transform.SetParent(marketPanel.transform);
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        obj.GetComponent<ProjectileUIHiglightController>().Set(nameText.color,normal,middle,powerful);
    }
    
}
