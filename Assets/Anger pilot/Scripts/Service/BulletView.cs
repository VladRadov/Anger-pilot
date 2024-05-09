using System.Collections;
using UnityEngine;
using UniRx;

public class BulletView : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    [SerializeField] protected Sprite _iconActive;
    [SerializeField] protected Sprite _iconNoActive;
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;

    public virtual void SetActive(bool value)
        => _spriteRenderer.sprite = value ? _iconActive : _iconNoActive;

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void StartMoveBullet()
        => _rigidbody.velocity = Vector2.right * _speedMove;

    public IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemyView = collision.collider.GetComponent<EnemyView>();
        if (enemyView != null)
        {
            enemyView.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }
}
