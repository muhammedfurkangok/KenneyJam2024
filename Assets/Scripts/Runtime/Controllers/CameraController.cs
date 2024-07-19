using Cinemachine;
using UnityEngine;
using Runtime.Managers;

namespace Runtime.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _freeLookCamera;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float minZoom = 10f;
        [SerializeField] private float maxZoom = 60f;

        private void Update()
        {
            SetCameraMovement();
            SetCameraZoom();
        }

        private void SetCameraMovement()
        {
            Vector2 movementInput = InputManager.Instance.GetMovementInput();
            Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y) * (moveSpeed * Time.deltaTime);
            _freeLookCamera.transform.position += direction;
        }

        private void SetCameraZoom()
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0.0f)
            {
                _freeLookCamera.m_Lens.FieldOfView -= scrollInput * zoomSpeed;
                _freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(_freeLookCamera.m_Lens.FieldOfView, minZoom, maxZoom);
            }
        }
    }
}