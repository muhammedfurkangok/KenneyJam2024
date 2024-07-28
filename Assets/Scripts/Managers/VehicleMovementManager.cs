using Entities.Vehicles;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class VehicleMovementManager : SingletonMonoBehaviour<VehicleMovementManager>
    {
        [Header("Info - No Touch")]
        [SerializeField] private VehicleBase currentSelectedVehicle;
        [SerializeField] private bool hasSelectedVehicle;

        private const int GroundLayerMask = 1 << 3;
        private const int VehicleLayerMask = 1 << 6;
        private const int BuildingLayerMask = 1 << 7;
        private const int ResourceLayerMask = 1 << 8;
        private const int GroundOrVehicleOrBuildingOrResourceLayerMask = GroundLayerMask | VehicleLayerMask | BuildingLayerMask | ResourceLayerMask;

        public VehicleType GetSelectedVehicleType() => currentSelectedVehicle.GetVehicleType();

        private void Update()
        {
            if (InputManager.Instance.GetInputMouseLeftClick())
            {
                if (GameManager.Instance.GetCurrentGameState() is not GameState.Free) return;

                if (!hasSelectedVehicle)
                {
                    if (RaycastManager.Instance.RaycastFromMousePosition(VehicleLayerMask, out var hit))
                    {
                        currentSelectedVehicle = hit.collider.GetComponent<VehicleBase>();
                        hasSelectedVehicle = true;

                        currentSelectedVehicle.Select();

                        GameManager.Instance.ChangeGameState(GameState.VehicleControl);
                        CursorManager.Instance.SetCursor(CursorType.VehicleControl);
                    }
                }

                else
                {
                    if (RaycastManager.Instance.RaycastFromMousePosition(GroundOrVehicleOrBuildingOrResourceLayerMask, out var hit))
                    {
                        if (hit.collider.gameObject.layer == 3) currentSelectedVehicle.Move(hit.point);
                    }
                }
            }

            else if (InputManager.Instance.GetInputMouseRightClick())
            {
                if (GameManager.Instance.GetCurrentGameState() is not GameState.VehicleControl) return;

                if (!hasSelectedVehicle) return;
                currentSelectedVehicle.Deselect();

                currentSelectedVehicle = null;
                hasSelectedVehicle = false;

                GameManager.Instance.ChangeGameState(GameState.Free);
                CursorManager.Instance.SetCursor(CursorType.Default);
            }
        }
    }
}