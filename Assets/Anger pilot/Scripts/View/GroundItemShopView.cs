using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemShopView : ItemShopView
{
    [SerializeField] private string _nameGround;

    public override void InitializeStateItem()
    {
        var isCurrentGround = ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentGround == _nameGround;
        var isPurchased = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenGrounds.Contains(_nameGround);

        if (isCurrentGround)
            SetSelectedState();
        else if (isPurchased)
            SetPurchasedState();
        else
            SetPurchaseState();
    }

    protected override void Purchase()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenGrounds += _nameGround + ";";
    }

    protected override void OnSelectedItem()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentGround = _nameGround;
        base.OnSelectedItem();
    }
}
