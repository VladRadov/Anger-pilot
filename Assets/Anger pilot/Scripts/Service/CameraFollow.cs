using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;

    private void FixedUpdate()
    {
        transform.position = new Vector3(_playerView.transform.position.x, 0, transform.position.z);
    }
}
