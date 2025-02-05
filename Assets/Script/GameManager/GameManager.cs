using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerCharecterV1_3 _playerCharecter;
    [SerializeField] private Pause _pause;
    [SerializeField] private ControlUi _controlUi;

    public void Pause()
    {
        _pause.OnPause(_playerCharecter);
        _controlUi.ControlFocusElement(_pause.Paused);
    }
}
