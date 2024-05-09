using UnityEngine;

[CreateAssetMenu(fileName = "SkinItem", menuName = "ScriptableObject/SkinItem")]
public class SkinItem : ItemShop
{
    [SerializeField] private Sprite _skinNoActive;

    public Sprite SkinNoActive => _skinNoActive;
}
