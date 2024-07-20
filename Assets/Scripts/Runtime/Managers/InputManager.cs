using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        [Header("Info - No Touch")]
        [SerializeField] private Vector2 cameraMovementInput;
        [SerializeField] private float mouseScrollInput;

        public Vector2 GetCameraMovementInput() => cameraMovementInput;
        public float GetMouseScrollInput() => mouseScrollInput;

        private void Update()
        {
            SetCameraMovementInput();
            SetMouseScrollInput();
        }

        private void SetMouseScrollInput()
        {
            mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        }

        private void SetCameraMovementInput()
        {
            cameraMovementInput.x = Input.GetAxis("Horizontal");
            cameraMovementInput.y = Input.GetAxis("Vertical");
        }
    }
}
