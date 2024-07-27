using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class RaycastManager : SingletonMonoBehaviour<RaycastManager>
    {
        [Header("References")]
        [SerializeField] private Camera mainCamera;

        public bool RaycastFromMousePosition(int layerMask, out RaycastHit hit)
        {
            hit = default; //Compiler gets angry about default value
            if (EventSystem.current.IsPointerOverGameObject()) return false; //Ignore UI Automatically

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, 50f, layerMask);
        }
    }
}