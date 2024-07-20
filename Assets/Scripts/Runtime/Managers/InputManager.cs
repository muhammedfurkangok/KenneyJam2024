using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        private Vector2 movementInput;
        private float mouseScrollInput;

        public float GetMouseScrollInput() => mouseScrollInput;
        public Vector2 GetMovementInput() => movementInput;

        private void Update()
        {
            SetVector2Input();
            SetMouseScrollInput();
        }

        private void SetMouseScrollInput()
        {
            mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        }

        private void SetVector2Input()
        {
            movementInput.x = Input.GetAxis("Horizontal");
            movementInput.y = Input.GetAxis("Vertical");
        }
    }
}
