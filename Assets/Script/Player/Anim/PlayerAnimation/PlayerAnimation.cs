using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private int _playerMovementAnimationID;
    private int _playerAttackAnimationID;

    private void SetupAnimationIDs()
    {
        _playerMovementAnimationID = Animator.StringToHash("Movement");
        _playerAttackAnimationID = Animator.StringToHash("Attack");
    }

    public void SetupPlayerAnimation()
    {
        _animator = GetComponentInChildren<Animator>();
        SetupAnimationIDs();
    }

    public void UpdateMovementAnimation(float movementBlendValue)
    {
        _animator.SetFloat(_playerMovementAnimationID, movementBlendValue);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_playerAttackAnimationID);
    }

}
