using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsAndPlayerInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private MyInputActions _inputActions;
    private IMoveble _playereble;

    private void Start()
    {
        _playereble = GetComponent<IMoveble>();
        _playerInput = GetComponent<PlayerInput>();
        //_playerInput.actions.
        //_inputActions = new MyInputActions();
        //_inputActions.Player.Enable();
        //Debug.Log("Set start - " + _playerInput.currentActionMap);
    }

    private void Update()
    {
        if (Mouse.current.forwardButton.wasPressedThisFrame)
        {
            _playerInput.SwitchCurrentActionMap("Test");//GamePlay //Ui
            Debug.Log("Set - Test");
        }

        if (Mouse.current.backButton.wasPressedThisFrame)
        {
            _playerInput.SwitchCurrentActionMap("Player");//GamePlay //Ui
            Debug.Log("Set - Player");
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            //Debug.Log($"  inputActions.Test.enabled {_inputActions.Test.enabled}\n inputActions.Player.enabled {_inputActions.Player.enabled}");

            //Debug.Log("Now - " + _playerInput.currentActionMap);
            Debug.Log(_playerInput.actions);
        }

    }
    //private void FixedUpdate()
    //{
    //    OnMove();
    //}

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

    //public void OnMove()
    //{
    //    Vector2 vector2 = _inputActions.Player.Move.ReadValue<Vector2>();
    //    _playereble.Move(new Vector3(vector2.x, 0, vector2.y));
    //}
}
