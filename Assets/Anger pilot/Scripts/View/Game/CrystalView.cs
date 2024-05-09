using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalView : MonoBehaviour
{
    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void SetActive(bool value)
        => transform.gameObject.SetActive(value);
}
