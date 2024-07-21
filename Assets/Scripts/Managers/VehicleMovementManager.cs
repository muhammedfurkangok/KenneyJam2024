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
        private const int GroundLayerMask = 1 << 3;

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
                    }
                }

                else
                {
                    if (Physics.Raycast(ray, out var hit, Mathf.Infinity, GroundLayerMask))
                    {
                        currentSelectedVehicle.Move(hit.point);
                    }
                }
            }

            else if (InputManager.Instance.GetInputMouseRightClick())
            {
                if (!hasSelectedVehicle) return;
                currentSelectedVehicle.Deselect();

                currentSelectedVehicle = null;
                hasSelectedVehicle = false;
            }
        }
    }
}