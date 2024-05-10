using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WalletManager
{
    public static bool TryPurchase(int price)
    {
        var isMach = ContainerSaveerPlayerPrefs.Instance.SaveerData.AllCrystal >= price;

        if (isMach)
            ContainerSaveerPlayerPrefs.Instance.SaveerData.AllCrystal -= price;

        return isMach;
    }
}
