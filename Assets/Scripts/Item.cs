using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField] public string DisplayName { get; private set; } = default;
    [field: SerializeField] public Sprite Sprite { get; private set; } = default;
}
