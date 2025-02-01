using UnityEngine;
using UnityEngine.InputSystem;

public class RebildControl : MonoBehaviour
{
    private const string actionAttack = "Attack";
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
   
    
    public void RebildActionButton(string actionName, MyInputActions actions)
    {
        if (actions.FindAction(actionName) == null) { return; }
        Rebild(actionName, actions);
    }

    public void ResetDefaltBilding(MyInputActions actions)
    {
        actions = new MyInputActions();
    }

    private void Rebild(string actionName, MyInputActions actions)
    {
         rebindOperation = actions.FindAction(actionName).PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .WithControlsExcluding("<Gamepad>/Start")
            .WithControlsExcluding("<Keyboard>/p")
            .WithControlsExcluding("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindCompleted());

        rebindOperation.Start();

    }

    private void RebindCompleted()
    {
        rebindOperation.Dispose();
        rebindOperation = null;

        //Ui отобразить
    }
}
