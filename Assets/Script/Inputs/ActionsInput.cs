using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsInput : MonoBehaviour
{
    private IPlayereble playereble;
    MyInputActions inputActions;

    private void Start()
    {
        inputActions = new MyInputActions();
        playereble = GetComponent<IPlayereble>();
        inputActions. Player.Jump.Enable();
        inputActions.Player.Move.Enable();
        inputActions.Player.Jump.performed += OnJump;
    }


    private void FixedUpdate()
    {
        if (inputActions.Player.Move.IsPressed())
        {
            OnMove();
        }
    }

    public void OnJump(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Performed)
        {
            playereble.Jump();
        }
    }

    public void OnMove()
    {
        Vector3 vector2 = inputActions.Player.Move.ReadValue<Vector2>();
        playereble.Move(new Vector3(vector2.x, 0, vector2.y));
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        Vector2 vector2 = callback.ReadValue<Vector2>();
        playereble.Move(vector2);
    }

}
