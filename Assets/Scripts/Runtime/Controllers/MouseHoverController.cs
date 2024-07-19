using Cinemachine;
using Runtime.Entities;
using UnityEngine;

namespace Runtime.Controllers
{
    public class MouseHoverController : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _freeLookCamera;
        private Camera _mainCamera;
        private HighlightableObject _highlightedObject;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            CheckForHighlight();
        }

        private void CheckForHighlight()
        {
            if (_mainCamera == null) return;

           
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

           
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
               
                HighlightableObject highlightableObject = hit.collider.GetComponent<HighlightableObject>();

                if (highlightableObject != null)
                {
                    if (_highlightedObject != highlightableObject)
                    {
                        RemoveHighlight();
                        _highlightedObject = highlightableObject;
                        _highlightedObject.Highlight();
                    }
                }
                else
                {
                    RemoveHighlight();
                }
            }
            else
            {
                RemoveHighlight();
            }
        }

        private void RemoveHighlight()
        {
            if (_highlightedObject != null)
            {
                _highlightedObject.RemoveHighlight();
                _highlightedObject = null;
            }
        }
    }
}