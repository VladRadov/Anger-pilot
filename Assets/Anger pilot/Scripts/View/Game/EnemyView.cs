using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private bool _isDead;

    [SerializeField] private int _damage;

    public int Damage => _damage;
    public bool IsDead => _isDead;

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void Dead()
    {
        SetActive(false);
        _isDead = true;
    }

    public void SetActive(bool value)
        => transform.gameObject.SetActive(value);

    private void OnEnable()
    {
        _isDead = false;
    }
}
