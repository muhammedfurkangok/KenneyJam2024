using Extensions;
using UnityEngine;

namespace Managers
{
    public class CursorManager : SingletonMonoBehaviour<CursorManager>
    {
        [Header("References")]
        [SerializeField] CursorTextureAndHotspot normalCursor;
        [SerializeField] CursorTextureAndHotspot selectableCursor;
        [SerializeField] CursorTextureAndHotspot vehicleTargetCursor;
        [SerializeField] CursorTextureAndHotspot disabledCursor;
        [SerializeField] CursorTextureAndHotspot mineCursor;

        [Header("Info - No Touch")]
        [SerializeField] private CursorType currentCursorType;

        private void Start()
        {
            SetNormalCursor();
        }

        private void Update()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.VehicleControl) DecideVehicleStateCursor();
        }

        public void SetNormalCursor()
        {
            if (currentCursorType == CursorType.Normal) return;

            Cursor.SetCursor(normalCursor.texture, normalCursor.hotspot, CursorMode.Auto);
            currentCursorType = CursorType.Normal;
        }

        public void SetSelectableCursor()
        {
            if (currentCursorType == CursorType.Selectable) return;

            Cursor.SetCursor(selectableCursor.texture, selectableCursor.hotspot, CursorMode.Auto);
            currentCursorType = CursorType.Selectable;
        }

        public void SetVehicleTargetCursor()
        {
            if (currentCursorType == CursorType.VehicleTarget) return;

            Cursor.SetCursor(vehicleTargetCursor.texture, vehicleTargetCursor.hotspot, CursorMode.Auto);
            currentCursorType = CursorType.VehicleTarget;
        }

        public void SetDisabledCursor()
        {
            if (currentCursorType == CursorType.Disabled) return;

            Cursor.SetCursor(disabledCursor.texture, disabledCursor.hotspot, CursorMode.Auto);
            currentCursorType = CursorType.Disabled;
        }

        public void SetMineCursor()
        {
            if (currentCursorType == CursorType.Mine) return;

            Cursor.SetCursor(mineCursor.texture, mineCursor.hotspot, CursorMode.Auto);
            currentCursorType = CursorType.Mine;
        }

        private void DecideVehicleStateCursor()
        {
            if (RaycastManager.Instance.RaycastFromScreenPoint(Input.mousePosition, 50f, Physics.AllLayers, out var hit))
            {
                if (hit.collider.gameObject.layer == 3) SetVehicleTargetCursor();
                else if (hit.collider.gameObject.layer == 8 && VehicleMovementManager.Instance.GetSelectedVehicleType() == VehicleType.Miner) SetMineCursor();
                else SetDisabledCursor();
            }

            else SetDisabledCursor();

        }
    }
}