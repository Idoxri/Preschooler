using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public event System.Action OnMaxCapacityReached;

    [field: SerializeField] public int Capacity { get; set; }

    private List<Item> items = new List<Item>();

    private void AddToBasket(Item itemToAdd)
    {
        items.Add(itemToAdd);

        if(items.Count >= Capacity)
        {
            OnMaxCapacityReached?.Invoke();
        }
    }
    private void RemoveFromBasket(Item itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent(out Item item))
        {
            AddToBasket(item);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent(out Item item) && items.Contains(item))
        {
            RemoveFromBasket(item);
        }
    }
}
