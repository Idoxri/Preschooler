using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CustomerHandlerEventHandler();
public class CustomerHandler : MonoBehaviour
{
    [SerializeField] private List<CustomerSetting> settingList = new List<CustomerSetting>();
    [SerializeField] private GameObject customerSpawnPoint = default;
    [SerializeField] private Customer customerPrefab = default;
    [SerializeField] private List<GameObject> itemSpawnPoint = default;

    public CustomerHandlerEventHandler OnComplete;

    public Customer CurrentCustomer { get; set; }
    public static CustomerHandler Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        NextCustomer();
    }

    private void NextCustomer()
    {
        CurrentCustomer = Instantiate(customerPrefab, customerSpawnPoint.transform.position, Quaternion.identity);
        CurrentCustomer.SetInformations(settingList[Random.Range(0, settingList.Count - 1)]);
        SpawnItem();
    }

    private void SpawnItem()
    {
        for (int i = 0; i < CurrentCustomer.ItemList.Count; i++)
        {
            Instantiate(CurrentCustomer.ItemList[i], itemSpawnPoint[i].transform.position, Quaternion.identity);
        }
    }
}
