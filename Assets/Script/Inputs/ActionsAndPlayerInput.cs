using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsAndPlayerInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private MyInputActions inputActions;
    private IPlayereble _playereble;

    private void Start()
    {
        _playereble = GetComponent<IPlayereble>();
        _playerInput = GetComponent<PlayerInput>();
        inputActions = new MyInputActions();
        inputActions.Player.Enable();
        Debug.Log("Set start - " + _playerInput.currentActionMap);
    }

    private void Update()
    {
        if (Mouse.current.forwardButton.wasPressedThisFrame)
        {
            _playerInput.SwitchCurrentActionMap("UI");//GamePlay //Ui
            Debug.Log("Set - Ui");
        }

        if (Mouse.current.backButton.wasPressedThisFrame)
        {
            _playerInput.SwitchCurrentActionMap("Player");//GamePlay //Ui
            Debug.Log("Set - Player");
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            Debug.Log($"  inputActions.Ui.enabled {inputActions.UI.enabled}\n inputActions.Player.enabled {inputActions.Player.enabled}");

            Debug.Log("Now - " + _playerInput.currentActionMap);
        }

    }
    private void FixedUpdate()
    {
        OnMove();
    }

    public void Spam(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Performed)
        {
            Debug.Log("Spam " + callback.phase);
        }
    }

    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Performed)
        {
            _playereble.Jump();
        }
    }

    //private void Move(InputAction.CallbackContext callback)
    //{
    //    Vector2 move = callback.ReadValue<Vector2>();
    //    _playereble.Move(new Vector3(move.x, 0, move.y));
    //}

    private void OnMove()
    {
        Vector3 vector2 = inputActions.Player.Move.ReadValue<Vector2>();
        _playereble.Move(new Vector3(vector2.x, 0, vector2.y));
    }
}
