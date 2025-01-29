using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharecterV1_3 : MonoBehaviour
{
    [SerializeField] private float _movementSmoothingSpeed = 1f;
    private MyInputActions _actions;
    private PlayerMove _playerMove;
    private PlayerAnimation _playerAnimation;
    private Vector3 _rawInputMovement;
    private Vector3 _smoothInputMovement;
    private const string actionMapPlayerControls = "Player";
    private const string actionMapMenuControls = "UI";

    private void OnEnable()
    {
        ControlPlayerSub(true);
        ControlUiSub(true);
    }

    private void OnDisable()
    {
        ControlPlayerSub(false);
        ControlUiSub(false);
    }

    private void Awake()
    {
        SetupPlayerCharecter();
        _playerMove.SetupPlayerMove();
        _playerAnimation.SetupPlayerAnimation();
        _actions = new MyInputActions();
        _actions.Player.Enable();
    }

    private void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdatePlayerAnimationMovement();
    }

    //private void Rebild()
    //{
    //    _actions.Player.Disable();
    //    _actions.Player.Attack.PerformInteractiveRebinding().OnComplete(callback =>
    //    {
    //        Debug.Log(callback);
    //        callback.Dispose();
    //        _actions.Player.Enable();
    //    })
    //        .Start();
    //}

    public void EnableGameplayControls()
    {
        _actions.Player.Enable();
        OnStopMove();
        _actions.UI.Disable();
    }

    public void EnablePauseMenuControls()
    {
        _actions.UI.Enable();
        OnStopMove();
        _actions.Player.Disable();
    }

    private void ControlPlayerSub(bool isSub)
    {
        if (isSub)
        {
            _actions.Player.Pause.started += OnPause;
            _actions.Player.Attack.started += OnAttack;
            _actions.Player.Move.performed += OnMovement;
            _actions.Player.Move.canceled += OnMovement;
        }
        else
        {
            _actions.Player.Pause.started -= OnPause;
            _actions.Player.Attack.started -= OnAttack;
            _actions.Player.Move.performed -= OnMovement;
            _actions.Player.Move.canceled -= OnMovement;
        }
    }

    private void ControlUiSub(bool isSub)
    {
        if (isSub)
        {
            _actions.UI.Pause.started += OnPause;
        }
        else
        {
            _actions.UI.Pause.started -= OnPause;
        }
    }

    public void OnPause(InputAction.CallbackContext value)
    {
        GameManager.Instance.Pause();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (value.phase == InputActionPhase.Performed)
        {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        }
        if (value.phase == InputActionPhase.Canceled)
        {
            OnStopMove();
        }
    }

    private void OnStopMove()
    {
        _rawInputMovement = Vector3.zero;
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.phase == InputActionPhase.Started)
        {
            _playerAnimation.PlayAttackAnimation();
        }
    }

    private void SetupPlayerCharecter()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void CalculateMovementInputSmoothing()
    {
        _smoothInputMovement = Vector3.Lerp(_smoothInputMovement, _rawInputMovement, Time.deltaTime * _movementSmoothingSpeed);
    }

    private void UpdatePlayerMovement()
    {
        _playerMove.UpdateMovementData(_smoothInputMovement);
    }

    private void UpdatePlayerAnimationMovement()
    {
        float magnitude = _smoothInputMovement.magnitude;
        if (magnitude < 0.001f) { magnitude = 0; }
        _playerAnimation.UpdateMovementAnimation(magnitude);
    }
}
