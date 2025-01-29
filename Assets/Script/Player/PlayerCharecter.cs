using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerCharecter : MonoBehaviour
{
    [SerializeField] private float movementSmoothingSpeed = 1f;
    MyInputActions actions;
    private PlayerMove _playerMove;
    private PlayerAnimation _playerAnimation;
    private PlayerInput _playerInput;
    private Vector3 _rawInputMovement;
    private Vector3 _smoothInputMovement;
    private const string actionMapPlayerControls = "Player";
    private const string actionMapMenuControls = "UI";

    void Start()
    {
        SetupPlayerCharecter();
        _playerMove.SetupPlayerMove();
        _playerAnimation.SetupPlayerAnimation();
         actions = new MyInputActions();
        _playerInput.actions = actions.asset;

        Debug.Log($"actions.UI -- {actions.UI.enabled}");
        Debug.Log($"actions.Player -- {actions.Player.enabled}");
        //_playerInput.actions.
    }

    private void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdatePlayerAnimationMovement();
    }

    public void EnableGameplayControls()
    {
        _playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
        Debug.Log($"actions.Player -- {actions.Player.enabled}");
        Debug.Log($"actions.UI -- {actions.UI.enabled}");
    }

    public void EnablePauseMenuControls()
    {
        _playerInput.SwitchCurrentActionMap(actionMapMenuControls);
        Debug.Log($"actions.Player -- {actions.Player.enabled}");
        Debug.Log($"actions.UI -- {actions.UI.enabled}");
        
    }

    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.phase==InputActionPhase.Performed)
        {
            GameManager.Instance.Pause();
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        Debug.Log($"state - {value.phase}  value - {inputMovement}");
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
        _playerInput = GetComponent<PlayerInput>();
    }

    private void CalculateMovementInputSmoothing()
    {
        _smoothInputMovement = Vector3.Lerp(_smoothInputMovement, _rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
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
