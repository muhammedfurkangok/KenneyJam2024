using System;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class CursorManager : SingletonMonoBehaviour<CursorManager>
    {
        [Header("References")]
        [SerializeField] private Texture2D normalCursor;
        [SerializeField] private Texture2D selectableCursor;
        [SerializeField] private Texture2D vehicleTargetCursor;
        [SerializeField] private Texture2D disabledCursor;
        [SerializeField] private Texture2D mineCursor;

        [Header("Info - No Touch")]
        [SerializeField] private CursorType currentCursorType;

        public CursorType GetCurrentCursorType() => currentCursorType;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            SetNormalCursor();
        }

        private void Update()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.VehicleControl) DecideVehicleStateCursor();
        }

        public void SetNormalCursor()
        {
            if (currentCursorType == CursorType.Normal) return;

            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
            currentCursorType = CursorType.Normal;
        }

        public void SetSelectableCursor()
        {
            if (currentCursorType == CursorType.Selectable) return;

            Cursor.SetCursor(selectableCursor, Vector2.zero, CursorMode.Auto);
            currentCursorType = CursorType.Selectable;
        }

        public void SetVehicleTargetCursor()
        {
            if (currentCursorType == CursorType.VehicleTarget) return;

            Cursor.SetCursor(vehicleTargetCursor, Vector2.zero, CursorMode.Auto);
            currentCursorType = CursorType.VehicleTarget;
        }

        public void SetDisabledCursor()
        {
            if (currentCursorType == CursorType.Disabled) return;

            Cursor.SetCursor(disabledCursor, Vector2.zero, CursorMode.Auto);
            currentCursorType = CursorType.Disabled;
        }

        public void SetMineCursor()
        {
            if (currentCursorType == CursorType.Mine) return;

            Cursor.SetCursor(mineCursor, Vector2.zero, CursorMode.Auto);
            currentCursorType = CursorType.Mine;
        }

        private void DecideVehicleStateCursor()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 50f))
            {
                if (hit.collider.gameObject.layer == 3) SetVehicleTargetCursor();
                else if (hit.collider.gameObject.layer == 8 && VehicleMovementManager.Instance.GetSelectedVehicleType() == VehicleType.Miner) SetMineCursor();
                else SetDisabledCursor();
            }

            else SetDisabledCursor();

        }
    }
}