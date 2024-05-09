using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class SystemInput : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool _isPauseGame;

    public ReactiveCommand OnMouseDownCommand = new ();
    public ReactiveCommand OnMouseUpCommand = new ();

    public void OnSetActivePause(bool value)
        => _isPauseGame = value;

    private void Start()
    {
        _isPauseGame = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == 0)
            OnMouseUpCommand.Execute();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == 0)
            OnMouseDownCommand.Execute();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnMouseDownCommand);
        ManagerUniRx.Dispose(OnMouseUpCommand);
    }
}
