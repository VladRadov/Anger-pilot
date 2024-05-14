using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalView : MonoBehaviour
{
    private Vector3 _positionOffcet = new Vector3(10, 0, 0);

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void SetActive(bool value)
        => transform.gameObject.SetActive(value);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerView>();
        if (player == null)
            transform.position += _positionOffcet;
    }
}
