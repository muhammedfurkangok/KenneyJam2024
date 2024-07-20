using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Managers;

namespace Entities
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class VehicleObject : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask vehicleLayer;
        [SerializeField] private Renderer renderer;
        [SerializeField] private Color highlightColor;
        [SerializeField] private float jumpHeight = 1f;
        [SerializeField] private float jumpDuration = 0.5f;

        private NavMeshAgent navMeshAgent;
        private Color originalColor;
        private Vector3 originalPosition;
        private Sequence jumpSequence;
        private bool isSelected = false;

        private static VehicleObject selectedVehicle; 

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            originalColor = renderer.material.color;
            originalPosition = transform.position;
        }

        private void Update()
        {
            if (InputManager.Instance.GetInputMouseLeftClick())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, vehicleLayer))
                {
                    if (hit.transform == transform)
                    {
                        ToggleSelection();
                    }
                    else if (isSelected)
                    {
                        DeselectObject();
                    }
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer) && isSelected)
                {
                    navMeshAgent.SetDestination(hit.point);
                    DeselectObject();
                }
            }
        }

        private void ToggleSelection()
        {
            if (isSelected)
            {
                DeselectObject();
            }
            else
            {
                if (selectedVehicle != null)
                {
                    selectedVehicle.DeselectObject();
                }
                SelectObject();
                selectedVehicle = this;
            }
        }

        private void SelectObject()
        {
            isSelected = true;
            Highlight();
            Jump();
        }

        private void DeselectObject()
        {
            isSelected = false;
            RemoveHighlight();
            if (selectedVehicle == this)
            {
                selectedVehicle = null;
            }
        }

        public void Highlight()
        {
            renderer.material.color = highlightColor;
        }

        public void RemoveHighlight()
        {
            renderer.material.color = originalColor;
        }

        private void Jump()
        {
            jumpSequence?.Kill();
            jumpSequence = DOTween.Sequence();
            jumpSequence.Append(transform.DOMoveY(originalPosition.y + jumpHeight, jumpDuration / 2).SetEase(Ease.OutQuad));
            jumpSequence.Append(transform.DOMoveY(originalPosition.y, jumpDuration / 2).SetEase(Ease.InQuad));
        }
    }
}
