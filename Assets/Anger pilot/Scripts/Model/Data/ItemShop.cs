using UnityEngine;

[CreateAssetMenu(fileName = "ItemShop", menuName = "ScriptableObject/ItemShop")]
public class ItemShop : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;

    public Sprite Icon => _icon;
    public string Name => _name;
}
