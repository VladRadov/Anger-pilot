using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HealthPlayerView : MonoBehaviour
{
    public ReactiveCommand OnGetHelthCommand = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthView>();
        if (health != null)
        {
            Destroy(collision.gameObject);
            OnGetHelthCommand.Execute();
        }
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnGetHelthCommand);
    }
}
