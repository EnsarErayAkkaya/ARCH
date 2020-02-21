using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerUI : MonoBehaviour
{
    public GameObject passangerGridView,passangerUIObject;
    PassangerManager passangerManager;    
    void OnEnable()
    {
        passangerManager = FindObjectOfType<PassangerManager>();
        SetPassangerUI();
    }
    public void SetPassangerUI()
    {
        ClearPassangerUI();
        for (int i = 0; i < passangerManager.maxPassangerCapacity; i++)
        {
            if(i < passangerManager.Passangers.Count)
            {
                //oluştur
                var obj = Instantiate(passangerUIObject).GetComponent<PassangerUIObjectController>();
                obj.Set( passangerManager.Passangers[i].passangerHealth);
                obj.transform.SetParent(passangerGridView.transform);
            }
            else
            {
                var obj = Instantiate(passangerUIObject).GetComponent<PassangerUIObjectController>();
                obj.Set(PassangerHealth.none);
                obj.transform.SetParent(passangerGridView.transform);
            }
            
        }
    }
    public void ClearPassangerUI()
    {
        foreach (Transform item in passangerGridView.transform )
        {
            Destroy(item.gameObject);
        }
    }
}
