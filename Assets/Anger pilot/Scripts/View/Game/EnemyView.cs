using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage => _damage;

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void SetActive(bool value)
        => transform.gameObject.SetActive(value);
}
