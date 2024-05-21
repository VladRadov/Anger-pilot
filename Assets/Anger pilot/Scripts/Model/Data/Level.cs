using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private List<FrameMapView> _prefabsFrameMapView;
    [SerializeField] private BatView _prefabBat;
    [SerializeField] private EnemyView _prefabWolf;
    [SerializeField] private BulletView _prefabBullet;
    [SerializeField] private CrystalView _prefabCrystal;
    [SerializeField] private SunView _prefabSun;

    public List<FrameMapView> PrefabsFrameMapView => _prefabsFrameMapView;
    public BatView PrefabBat => _prefabBat;
    public EnemyView PrefabWolf => _prefabWolf;
    public BulletView PrefabBullet => _prefabBullet;
    public CrystalView PrefabCrystal => _prefabCrystal;
    public SunView PrefabSun => _prefabSun;
}
