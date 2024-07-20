using Data.ScriptableObjects;
using UnityEngine;
using DG.Tweening;
using Managers;

namespace Runtime.Entities
{
    public class HighlightableObject : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EntityParameters paramData;
        [SerializeField] private Renderer renderer;

        private Color originalColor;
        private Vector3 originalPosition;
        private Sequence jumpSequence;

        private void Awake()
        {
            originalColor = renderer.material.color;
            originalPosition = transform.position;
        }

        public void Highlight()
        {
            renderer.material.color = paramData.highlightColor;
        }

        public void RemoveHighlight()
        {
            renderer.material.color = originalColor;
        }

        private void OnMouseDown()
        {
            if (UIManager.Instance.GetIsUIActive())
            {
                UIManager.Instance.HideUI();
            }

            else
            {
                Jump();
                UIManager.Instance.OpenUI();
            }
        }

        private void Jump()
        {
            jumpSequence?.Kill();
            jumpSequence = DOTween.Sequence();
            jumpSequence.Append(transform.DOMoveY(originalPosition.y + paramData.jumpHeight, paramData.jumpDuration / 2).SetEase(Ease.OutQuad));
            jumpSequence.Append(transform.DOMoveY(originalPosition.y, paramData.jumpDuration / 2).SetEase(Ease.InQuad));
        }
    }
}