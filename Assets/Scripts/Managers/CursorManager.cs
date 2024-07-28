using System;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class CursorManager : SingletonMonoBehaviour<CursorManager>
    {
        [Header("References")]
        [SerializeField] private CursorInfo[] cursorInfos;

        [Header("Info - No Touch")]
        [SerializeField] private CursorType currentCursorType;
        [SerializeField] private CursorType previousCursorType;

        public CursorType GetCurrentCursorType() => currentCursorType;
        public CursorType GetPreviousCursorType() => previousCursorType;

        private void Start()
        {
            SetCursor(CursorType.Default);
        }

        private void Update()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.VehicleControl) DecideVehicleStateCursor();
        }

        public void SetCursor(CursorType type)
        {
            var cursorInfo = GetCursor(type);
            if (currentCursorType == type) return;

            Cursor.SetCursor(cursorInfo.texture, cursorInfo.hotspot, CursorMode.Auto);

            previousCursorType = currentCursorType;
            currentCursorType = type;
        }

        public CursorInfo GetCursor(CursorType type)
        {
            foreach (var cursorInfo in cursorInfos)
            {
                if (cursorInfo.type == type) return cursorInfo;
            }

            throw new Exception("Cursor type not found: " + type);
        }

        private void DecideVehicleStateCursor()
        {
            if (RaycastManager.Instance.RaycastFromMousePosition(Physics.AllLayers, out var hit))
            {
                if (hit.collider.gameObject.layer == 3) SetCursor(CursorType.VehicleControl);
                else if (hit.collider.gameObject.layer == 8 && VehicleMovementManager.Instance.GetSelectedVehicleType() == VehicleType.Miner) SetCursor(CursorType.Mine);
                else SetCursor(CursorType.Disabled);
            }

            else SetCursor(CursorType.Disabled);

        }
    }
}