using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyPlaceEnemyView : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;

    public bool IsDeadEnemy => _enemyView.IsDead;

    public void DeadEnemy()
        => _enemyView.Dead();
}
