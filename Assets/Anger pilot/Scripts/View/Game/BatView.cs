using UnityEngine;

public class BatView : EnemyView
{
    private Transform _player;
    private Transform _transform;

    [Header("Мин. дистанция для атаки")]
    [SerializeField] private float _minDistanceAttack;

    public void SetTransformPlayer(Transform player)
        => _player = player;

    private void OnBecameVisible()
    {
        AudioManager.Instance.PlayBat();
    }

    private void Start()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(_player.position, _transform.position) < _minDistanceAttack)
            _transform.position = Vector2.MoveTowards(_transform.position, _player.position, 0.2f);
    }
}
