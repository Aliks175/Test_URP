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
        var a = InputSystem.actions.actionMaps;
        for (int i = 0; i < a.Count; i++)
        {
                Debug.Log($" InputSystem.actions.actionMaps {i} {a[i].name} " + a[i].enabled);
        }
        
        _actionMapMenu.Disable();
    }
   
    private void Update()
    {
        if (_actionMapMenu.enabled != true)
        {
                Debug.Log($" _actionMapMenu - Off ");
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            var a = InputSystem.actions.actionMaps;
            for (int i = 0; i < a.Count; i++)
            {
                Debug.Log($" InputSystem.actions.actionMaps {i} {a[i].name} " + a[i].enabled);
            }

        }
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
        //value.control вызыв 2 раза перебивает друг друга
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
            Debug.Log(value.phase);
            _playerAnimation.PlayAttackAnimation();

            Debug.Log(value.control);
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
        //_smoothInputMovement = Vector3.SmoothDamp(_smoothInputMovement, _rawInputMovement);
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
