using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharecterV1_3 : MonoBehaviour
{
    [SerializeField] private float _movementSmoothingSpeed = 1f;

    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _pauseAction;
    private InputActionMap _actionMapPlayer;
    private InputActionMap _actionMapMenu;

    private PlayerMove _playerMove;
    private PlayerAnimation _playerAnimation;
    private Vector3 _rawInputMovement;
    private Vector3 _smoothInputMovement;

    private const string actionMapPlayerControls = "Player";
    private const string actionMapMenuControls = "UI";

    private void OnEnable()
    {
        _attackAction.started += OnAttack;
        _actionMapMenu.FindAction("Pause").performed += OnPause;
        _actionMapPlayer.FindAction("Pause").performed += OnPause;

        _moveAction.performed += OnMovement;
        _moveAction.canceled += OnMovement;

    }

    private void OnDisable()
    {
        _actionMapMenu.FindAction("Pause").performed -= OnPause;
        _actionMapPlayer.FindAction("Pause").performed -= OnPause;
        _attackAction.started -= OnAttack;
        _moveAction.performed -= OnMovement;
        _moveAction.canceled -= OnMovement;
    }

    private void Awake()
    {
        SetUpPlayerCharecterV1_3();
        SetupPlayerCharecter();
        _playerMove.SetupPlayerMove();
        _playerAnimation.SetupPlayerAnimation();
        
        Invoke("WW", 0.1f);
    }
   
    private void WW()
    {
        _actionMapMenu.Disable();
    }

    private void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdatePlayerAnimationMovement();
    }

    private void SetUpPlayerCharecterV1_3()
    {
        _actionMapPlayer = InputSystem.actions.FindActionMap(actionMapPlayerControls);
        _actionMapMenu = InputSystem.actions.FindActionMap(actionMapMenuControls);
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    public void EnableGameplayControls()
    {
        _actionMapPlayer.Enable();
        _actionMapMenu.Disable();
        OnStopMove();
    }

    public void EnablePauseMenuControls()
    {
        _actionMapPlayer.Disable();
        _actionMapMenu.Enable();
        OnStopMove();
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
