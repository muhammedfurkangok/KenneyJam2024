using UnityEngine;
using DG.Tweening;
using Runtime.Managers;

namespace Runtime.Entities
{
    public class HighlightableObject : MonoBehaviour
    {
        private Renderer _renderer;
        private Color _originalColor;
        [SerializeField] private Color highlightColor = Color.yellow;

        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight = 0.3f;
        [SerializeField] private float jumpDuration = 0.2f;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _originalColor = _renderer.material.color;
        }

        public void Highlight()
        {
            _renderer.material.color = highlightColor;
        }

        public void RemoveHighlight()
        {
            _renderer.material.color = _originalColor;
        }

        private void OnMouseDown()
        {
            if (UIManager.Instance.IsUIActive())
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
            Vector3 originalPosition = transform.position;
            transform.DOMoveY(originalPosition.y + jumpHeight, jumpDuration / 2).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                transform.DOMoveY(originalPosition.y, jumpDuration / 2).SetEase(Ease.InQuad);
            });
        }
    }
}