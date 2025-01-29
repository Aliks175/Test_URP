using UnityEngine;

public interface IMoveble
{
    void Jump();
    void Move(Vector3 vector2);
}

public class ControlTest : MonoBehaviour, IMoveble
{
    [SerializeField] private float _speed;
    [SerializeField] private float _heightJump;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        
    }


    public void Move(Vector3 vector2)
    {
        //_rigidbody.AddForce(vector2 * _speed, ForceMode.Impulse);
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _heightJump, ForceMode.Impulse);
    }
}
