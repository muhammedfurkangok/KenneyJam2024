using System;
using Runtime.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
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
        public bool GetInputShift() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        public bool GetInputMouseLeftClick() => Input.GetMouseButtonDown(0);
        public Vector3 GetMousePosition() => Input.mousePosition;
        
        public event Action OnGridClick, OnGridExit;
        
        public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
            

        private void Update()
        {
            SetGridInput();
            SetCameraMovementInput();
            SetCameraRotateInput();
            SetMouseScrollInput();
        }

        private void SetGridInput()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                OnGridClick?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                OnGridExit?.Invoke();
            }
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

        public Vector3 GetSelectedMapPosition(LayerMask placementLayerMask)
        {
            Vector3 mousepos = Input.mousePosition;
            mousepos.z = Camera.main.nearClipPlane;
            Ray ray = Camera.main.ScreenPointToRay(mousepos);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,placementLayerMask))
            {
                return hit.point;
            }

            return Vector3.zero;
        }

        public float GetSensitivity02()
        {
            var sensitivity = PlayerPrefs.GetInt(PlayerPrefsStringHolder.SensitivityInt, 50);
            return sensitivity / 50f;
        }
    }
}
