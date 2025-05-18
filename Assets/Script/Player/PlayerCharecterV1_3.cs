using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerMove), typeof(PlayerAnimation))]
public class PlayerCharecterV1_3 : MonoBehaviour
{
    [SerializeField] private float _movementSmoothingSpeed = 1f;

    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _pauseAction;
    private InputActionMap _actionMapPlayer;
    private InputActionMap _actionMapMenu;

    private PlayerHealth _playerHealth;
    private PlayerMove _playerMove;
    private PlayerAnimation _playerAnimation;
    private Vector3 _rawInputMovement;
    private Vector3 _smoothInputMovement;

    private Coroutine _coroutine;

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
        SetupPlayerCharecter();
        SetUpPlayerCharecterV1_3();
        _playerMove.SetupPlayerMove();
        _playerAnimation.SetupPlayerAnimation();
        _playerHealth.Respawn();
        
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

        _playerHealth.OnHited += TakeDamage;
        _playerHealth.OnDied += OnDied;
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

            if (Gamepad.current!=null)
            {
              Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
    }

    private void TakeDamage()
    {
        _playerAnimation.PlayGetDamageAnimation();
        _actionMapPlayer.Disable();
        OnStopMove();

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(OnDizzy());
        }

    }

    private void OnDied()
    {
        _playerAnimation.PlayDieAnimation();

        _coroutine = StartCoroutine(OnRespawn());
    }

    private IEnumerator OnRespawn()
    {
        yield return new WaitForSeconds(5f);
        _actionMapPlayer.Enable();
        _playerHealth.Respawn();
        _playerAnimation.PlayRespawnAnimation();
        _coroutine = null;
    }

    private IEnumerator OnDizzy()
    {
        yield return new WaitForSeconds(2f);
        _actionMapPlayer.Enable();
        _coroutine = null;
    }




    private void SetupPlayerCharecter()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerHealth = GetComponent<PlayerHealth>();
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
