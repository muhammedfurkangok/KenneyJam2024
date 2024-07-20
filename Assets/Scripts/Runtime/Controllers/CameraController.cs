using Cinemachine;
using UnityEngine;
using Runtime.Managers;

namespace Runtime.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineFreeLook freeLookCamera;

        [Header("Parameters")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;

        private void Update()
        {
            SetCameraMovement();
            SetCameraZoom();
        }

        private void SetCameraMovement()
        {
            var movementInput = InputManager.Instance.GetCameraMovementInput();
            var direction = new Vector3(movementInput.x, 0, movementInput.y) * (moveSpeed * Time.deltaTime);
            freeLookCamera.transform.position += direction;
        }

        private void SetCameraZoom()
        {
            var scrollInput = InputManager.Instance.GetMouseScrollInput();
            if (scrollInput == 0.0f) return;

            freeLookCamera.m_Lens.FieldOfView -= scrollInput * zoomSpeed;
            freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(freeLookCamera.m_Lens.FieldOfView, minZoom, maxZoom);
        }
    }
}