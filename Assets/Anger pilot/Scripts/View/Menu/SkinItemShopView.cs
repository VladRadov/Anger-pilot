using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinItemShopView : ItemShopView
{
    [SerializeField] private SkinItem _skinItem;

    public override void InitializeStateItem()
    {
        var isCurrentSkin = ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin == _skinItem.Name;
        var isPurchased = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins.Contains(_skinItem.Name);

        if (isCurrentSkin)
            SetSelectedState();
        else if (isPurchased)
            SetPurchasedState();
        else
            SetPurchaseState();
    }

    protected override void Purchase()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins += _skinItem.Name + ";";
    }

    protected override void OnSelectedItem()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin = _skinItem.Name;
        base.OnSelectedItem();
    }
}
