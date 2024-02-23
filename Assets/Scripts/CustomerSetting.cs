using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CustomerSetting", menuName = "ScriptableObjects/Customer")]
public class CustomerSetting : ScriptableObject
{
    [field : SerializeField] public List<GameObject> ItemList { get; private set; }
}
