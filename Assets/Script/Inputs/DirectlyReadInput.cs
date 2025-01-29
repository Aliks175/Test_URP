using UnityEngine;
using UnityEngine.InputSystem;

public class DirectlyReadInput : MonoBehaviour
{
    private IMoveble _playereble;
    private Vector3 _movement;
    private bool _isJump = false;

    private void Start()
    {
        _playereble = GetComponent<IMoveble>();
    }

    private void Update()
    {
        if (Keyboard.current.anyKey == null) { return; }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _isJump = true;
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            Moveble moveble = new Moveble();
            if (Keyboard.current.wKey.isPressed)
            {
                moveble.Ypos = 1;
            }
            if (Keyboard.current.sKey.isPressed)
            {
                moveble.Ypos = -1;
            }
            if (Keyboard.current.aKey.isPressed)
            {
                moveble.Xpos = -1;
            }
            if (Keyboard.current.dKey.isPressed)
            {
                moveble.Xpos = 1;
            }
            _movement = moveble.Movement;
        }
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            _playereble.Jump();
            _isJump = false;
        }
        if (_movement != Vector3.zero)
        {
            _playereble.Move(_movement);
            _movement = Vector3.zero;
        }
    }

    private struct Moveble
    {
        public float Xpos;
        public float Ypos;
        public Vector3 Movement => new Vector3(Xpos, 0, Ypos);
    }
}
