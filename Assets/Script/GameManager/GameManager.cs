using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerCharecterV1_3 _playerCharecter;
    [SerializeField] private Pause _pause;
    [SerializeField] private RebildControl _rebildControl;
    [SerializeField] private ControlUi _controlUi;


    public void RebildActionButton(string actionName)
    {
        _rebildControl.RebildActionButton(actionName, _playerCharecter.GetMyInputActions());
    }

    public void DefaltBilding() 
    {
        _rebildControl.ResetDefaltBilding(_playerCharecter.GetMyInputActions());
    }

    public void Pause()
    {
        _pause.OnPause(_playerCharecter);
        _controlUi.ControlFocusElement(_pause.Paused);
    }
}
