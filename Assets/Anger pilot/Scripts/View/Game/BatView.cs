using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatView : EnemyView
{
    private void OnBecameVisible()
    {
        AudioManager.Instance.PlayBat();
    }
}
