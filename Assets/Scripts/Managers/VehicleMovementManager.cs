using Entities.Vehicles;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class VehicleSelectionManager : SingletonMonoBehaviour<VehicleSelectionManager>
    {
        [Header("Info - No Touch")]
        [SerializeField] private VehicleBase currentSelectedVehicle;
        [SerializeField] private bool hasSelectedVehicle;

        private Camera mainCamera;

        private const int VehicleLayerMask = 1 << 6;
        private const int BuildingLayerMask = 1 << 7;
        private const int GroundLayerMask = 1 << 3;
        private const int VehicleOrBuildingOrGroundLayerMask = VehicleLayerMask | BuildingLayerMask | GroundLayerMask;

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
                    if (Physics.Raycast(ray, out var hit, Mathf.Infinity, VehicleLayerMask))
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
                    if (Physics.Raycast(ray, out var hit, Mathf.Infinity, VehicleOrBuildingOrGroundLayerMask))
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