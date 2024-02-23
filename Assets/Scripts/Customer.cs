using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private CustomerSetting customerSetting;

    public List<GameObject> ItemList { get; private set; } = new List<GameObject>();

    public void SetInformations(CustomerSetting setting)
    {
        customerSetting = setting;

        for (int i = 0; i < customerSetting.ItemList.Count; i++)
        {
            ItemList.Add(customerSetting.ItemList[i]);
        }
    }
}
