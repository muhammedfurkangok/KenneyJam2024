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

        private Camera mainCamera;

        private const int GroundLayerMask = 1 << 3;
        private const int VehicleLayerMask = 1 << 6;
        private const int BuildingLayerMask = 1 << 7;
        private const int ResourceLayerMask = 1 << 8;
        private const int GroundOrVehicleOrBuildingOrResourceLayerMask = GroundLayerMask | VehicleLayerMask | BuildingLayerMask | ResourceLayerMask;

        public VehicleType GetSelectedVehicleType() => currentSelectedVehicle.GetVehicleType();

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (InputManager.Instance.GetInputMouseLeftClick())
            {
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (!hasSelectedVehicle)
                {
                    if (Physics.Raycast(ray, out var hit, 50f, VehicleLayerMask))
                    {
                        currentSelectedVehicle = hit.collider.GetComponent<VehicleBase>();
                        hasSelectedVehicle = true;

                        currentSelectedVehicle.Select();

                        GameManager.Instance.ChangeGameState(GameState.VehicleControl);
                        CursorManager.Instance.SetVehicleTargetCursor();
                    }
                }

                else
                {
                    if (Physics.Raycast(ray, out var hit, 50f, GroundOrVehicleOrBuildingOrResourceLayerMask))
                    {
                        if (hit.collider.gameObject.layer == 3) currentSelectedVehicle.Move(hit.point);
                    }
                }
            }

            else if (InputManager.Instance.GetInputMouseRightClick())
            {
                if (!hasSelectedVehicle) return;
                currentSelectedVehicle.Deselect();

                currentSelectedVehicle = null;
                hasSelectedVehicle = false;

                GameManager.Instance.ChangeGameState(GameState.Free);
                CursorManager.Instance.SetNormalCursor();
            }
        }
    }
}