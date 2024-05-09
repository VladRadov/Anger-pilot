using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int _countFrame;
    [SerializeField] private FrameMapView _prefabFrameMapView;
    [SerializeField] private EnemyView _prefabBat;
    [SerializeField] private EnemyView _prefabWolf;

    public int CountFrame => _countFrame;
    public FrameMapView PrefabFrameMapView => _prefabFrameMapView;
    public EnemyView PrefabBat => _prefabBat;
    public EnemyView PrefabWolf => _prefabWolf;
}
