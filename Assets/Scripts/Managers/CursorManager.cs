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

        private Camera mainCamera;

        private const int VehicleLayerMask = 1 << 6;
        private const int BuildingLayerMask = 1 << 7;
        private const int GroundLayerMask = 1 << 3;
        private const int VehicleOrBuildingOrGroundLayerMask = VehicleLayerMask | BuildingLayerMask | GroundLayerMask;

        private void Start()
        {
            mainCamera = Camera.main;

            SetNormalCursor();
        }

        private void Update()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.VehicleControl) DecideVehicleTargetOrDisableCursor();
        }

        public void SetNormalCursor()
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }

        public void SetSelectableCursor()
        {
            Cursor.SetCursor(selectableCursor, Vector2.zero, CursorMode.Auto);
        }

        public void SetVehicleTargetCursor()
        {
            Cursor.SetCursor(vehicleTargetCursor, Vector2.zero, CursorMode.Auto);
        }

        public void SetDisabledCursor()
        {
            Cursor.SetCursor(disabledCursor, Vector2.zero, CursorMode.Auto);
        }

        private void DecideVehicleTargetOrDisableCursor()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 50f, VehicleOrBuildingOrGroundLayerMask))
            {
                if (hit.collider.gameObject.layer == 3) SetVehicleTargetCursor();
                else SetDisabledCursor();
            }

            else SetDisabledCursor();

        }
    }
}