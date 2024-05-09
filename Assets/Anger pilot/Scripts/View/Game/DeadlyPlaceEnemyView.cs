using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyPlaceEnemyView : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;

    public void SetActiveEnemy(bool value)
        => _enemyView.SetActive(value);
}
