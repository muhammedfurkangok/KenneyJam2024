using Entities;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class HighlightManager : SingletonMonoBehaviour<HighlightManager>
    {
        [Header("Info - No Touch")]
        private Highlightable currentHighlightable;
        private bool hasHighlightedObject;

        private const int VehicleLayerMask = 1 << 6;
        private const int BuildingLayerMask = 1 << 7;
        private const int VehicleOrBuildingLayerMask = VehicleLayerMask | BuildingLayerMask;

        private void Update()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.Free) CheckForHighlight();
        }

        private void CheckForHighlight()
        {
            if (RaycastManager.Instance.RaycastFromMousePosition(VehicleOrBuildingLayerMask, out var hit))
            {
                var hitHighlightable = hit.collider.GetComponent<Highlightable>();
                if (currentHighlightable == hitHighlightable) return;

                if (hasHighlightedObject) currentHighlightable.RemoveHighlight();
                currentHighlightable = hitHighlightable;
                currentHighlightable.Highlight();

                hasHighlightedObject = true;

                CursorManager.Instance.SetCursor(CursorType.Selectable);
            }

            else ResetHighlightable();
        }

        private void ResetHighlightable()
        {
            if (!hasHighlightedObject) return;
            currentHighlightable.RemoveHighlight();

            hasHighlightedObject = false;
            currentHighlightable = null;

            CursorManager.Instance.SetCursor(CursorType.Default);
        }
    }
}