using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfView : MonoBehaviour
{
    private void OnBecameVisible()
    {
        AudioManager.Instance.PlayWolf();
    }
}
