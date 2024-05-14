using System.Collections;
using UnityEngine;
using UniRx;

public class BulletView : MonoBehaviour
{
    private bool _isMove;
    private Vector3 _positionOffcet = new Vector3(10, 0, 0);

    [SerializeField] private float _speedMove;
    [SerializeField] protected Sprite _iconActive;
    [SerializeField] protected Sprite _iconNoActive;
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;

    public bool InMove => _isMove;

    public virtual void SetActiveIcon(bool value)
        => _spriteRenderer.sprite = value ? _iconActive : _iconNoActive;

    public void SetActive(bool value)
        => transform.gameObject.SetActive(value);

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void StartMoveBullet()
        => _rigidbody.velocity = Vector2.right * _speedMove;

    public void SetMove(bool value)
        => _isMove = value;

    public IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyView = collision.gameObject.GetComponent<EnemyView>();
        if (_isMove && enemyView != null)
        {
            enemyView.SetActive(false);
            gameObject.SetActive(false);
            AudioManager.Instance.PlayHitBullet();
            AudioManager.Instance.PlayLaugh();
        }

        var player = collision.gameObject.GetComponent<PlayerView>();
        if (player != null && _isMove == false)
            transform.position += _positionOffcet;
    }

    private void OnValidate()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }
}
