using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemShopView : ItemShopView
{
    [SerializeField] private ItemShop _ground;

    public override void InitializeStateItem()
    {
        var isCurrentGround = ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentGround == _ground.Name;
        var isPurchased = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenGrounds.Contains(_ground.Name);

        if (isCurrentGround)
            SetSelectedState();
        else if (isPurchased)
            SetPurchasedState();
        else
            SetPurchaseState();
    }

    protected override void Purchase()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenGrounds += _ground.Name + ";";
    }

    protected override void OnSelectedItem()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentGround = _ground.Name;
        base.OnSelectedItem();
    }
}
