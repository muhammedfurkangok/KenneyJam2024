using Cinemachine;
using Runtime.Entities;
using UnityEngine;

namespace Runtime.Controllers
{
    public class MouseHoverController : MonoBehaviour
    {
        private Camera mainCamera;
        private HighlightableObject highlightedObject;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            CheckForHighlight();
        }

        private void CheckForHighlight()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var highlightableObject = hit.collider.GetComponent<HighlightableObject>();

                if (highlightableObject != null)
                {
                    if (highlightedObject != highlightableObject)
                    {
                        RemoveHighlight();

                        highlightedObject = highlightableObject;
                        highlightedObject.Highlight();
                    }
                }

                else RemoveHighlight();
            }

            else RemoveHighlight();
        }

        private void RemoveHighlight()
        {
            if (highlightedObject == null) return;

            highlightedObject.RemoveHighlight();
            highlightedObject = null;
        }
    }
}