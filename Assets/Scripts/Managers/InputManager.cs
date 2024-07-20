using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        [Header("Info - No Touch")]
        [SerializeField] private Vector2 cameraMovementInput;
        [SerializeField] private float cameraHorizontalRotateInput;
        [SerializeField] private float cameraVerticalRotateInput;
        [SerializeField] private float mouseScrollInput;

        public Vector2 GetCameraMovementInput() => cameraMovementInput;
        public float GetCameraHorizontalRotateInput() => cameraHorizontalRotateInput;
        public float GetCameraVerticalRotateInput() => cameraVerticalRotateInput;
        public float GetMouseScrollInput() => mouseScrollInput;

        private void Update()
        {
            SetCameraMovementInput();
            SetCameraRotateInput();
            SetMouseScrollInput();
        }

        private void SetMouseScrollInput()
        {
            mouseScrollInput = Input.GetAxisRaw("Mouse ScrollWheel");
        }

        private void SetCameraRotateInput()
        {
            cameraHorizontalRotateInput = Input.GetMouseButton(1) ? Input.GetAxisRaw("Mouse X") : 0.0f;
            cameraVerticalRotateInput = Input.GetMouseButton(1) ? Input.GetAxisRaw("Mouse Y") : 0.0f;
        }

        private void SetCameraMovementInput()
        {
            cameraMovementInput.x = Input.GetAxisRaw("Horizontal");
            cameraMovementInput.y = Input.GetAxisRaw("Vertical");
        }
    }
}
