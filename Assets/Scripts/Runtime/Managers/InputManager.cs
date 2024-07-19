using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        private Vector2 _movementInput;

        private void Update()
        {
            SetVector2Input();
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