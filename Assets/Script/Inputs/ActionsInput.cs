using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsInput : MonoBehaviour
{
    private IPlayereble _playereble;
    private MyInputActions _inputActions;

    private void Start()
    {
        _inputActions = new MyInputActions();
        _playereble = GetComponent<IPlayereble>();
        _inputActions.Player.Enable();
        _inputActions.Test.Enable();
        _inputActions.Player.Jump.performed += OnJump;
        _inputActions.Test.SendMasage.performed += Spam;
    }


    private void FixedUpdate()
    {
        if (_inputActions.Player.Move.IsPressed())
        {
            OnMove();
        }
    }

    public void OnJump(InputAction.CallbackContext callback)
    {
            _playereble.Jump();
    }

    public void OnMove()
    {
        Vector2 vector2 = _inputActions.Player.Move.ReadValue<Vector2>();
        _playereble.Move(new Vector3(vector2.x, 0, vector2.y));
    }

    private void Spam(InputAction.CallbackContext callback)
    {
            Debug.Log("ActionsInput Spam " + callback.phase);
    }
}
