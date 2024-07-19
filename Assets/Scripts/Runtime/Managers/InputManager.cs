using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        private Vector2 _movementInput;
        private float _mouseScrollInput;

        private void Update()
        {
            SetVector2Input();
            SetMouseScrollInput();
        }

        private void SetMouseScrollInput()
        {
            _mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        }
        
        public float GetMouseScrollInput()
        {
            return _mouseScrollInput;
        }

        private void SetVector2Input()
        {
            _movementInput.x = Input.GetAxis("Horizontal");
            _movementInput.y = Input.GetAxis("Vertical");
        }

        public Vector2 GetMovementInput()
        {
            return _movementInput;
        }
        
    }
}