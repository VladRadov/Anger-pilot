using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerController
{
    private PlayerView _view;
    private Player _player;

    public PlayerController(PlayerView view, Player player)
    {
        _view = view;
        _player = player;
    }

    public void Initialize()
    {
        _player.Speed.Subscribe(value => { _view.UpdateSpeed(value); });
    }

    public void Dispose()
    {
        ManagerUniRx.Dispose(_player.Speed);
    }
}
