using Cinemachine;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
        [SerializeField] private Transform cameraRotateTransform;

        [Header("Parameters")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        [SerializeField] private float maxVerticalRotation;
        [SerializeField] private float minVerticalRotation;

        private void Update()
        {
            SetCameraMovement();
            SetCameraHorizontalRotation();
            //SetCameraVerticalRotation();
            SetCameraZoom();
        }

        private void SetCameraMovement()
        {
            var movementInput = InputManager.Instance.GetCameraMovementInput();
            var effectiveMoveSpeed = InputManager.Instance.GetInputShift() ? moveSpeed * 2 : moveSpeed;

            var direction = cinemachineCamera.transform.forward * movementInput.y + cinemachineCamera.transform.right * movementInput.x;
            direction.y = 0.0f;
            direction.Normalize();

            var positionChangeAmount = direction * (effectiveMoveSpeed * Time.deltaTime);
            cinemachineCamera.transform.position += positionChangeAmount;
            cameraRotateTransform.position += positionChangeAmount;
        }

        private void SetCameraHorizontalRotation()
        {
            var rotateInput = InputManager.Instance.GetCameraHorizontalRotateInput();
            if (rotateInput == 0.0f) return;

            cinemachineCamera.transform.RotateAround(cameraRotateTransform.position, Vector3.up, rotateInput * rotationSpeed);
        }

        private void SetCameraVerticalRotation()
        {
            var rotateInput = InputManager.Instance.GetCameraVerticalRotateInput();
            if (rotateInput == 0.0f) return;

            var currentEulerX = cinemachineCamera.transform.eulerAngles.x;
            var newEulerX = Mathf.Clamp(currentEulerX - rotateInput * rotationSpeed, minVerticalRotation, maxVerticalRotation);

            var euler = cinemachineCamera.transform.eulerAngles;
            euler.x = newEulerX;
            cinemachineCamera.transform.eulerAngles = euler;
        }

        private void SetCameraZoom()
        {
            var scrollInput = InputManager.Instance.GetMouseScrollInput();
            if (scrollInput == 0.0f) return;

            cinemachineCamera.m_Lens.FieldOfView -= scrollInput * zoomSpeed;
            cinemachineCamera.m_Lens.FieldOfView = Mathf.Clamp(cinemachineCamera.m_Lens.FieldOfView, minZoom, maxZoom);
        }
    }
}
