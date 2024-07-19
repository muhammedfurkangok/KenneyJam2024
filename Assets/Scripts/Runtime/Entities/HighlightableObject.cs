using UnityEngine;

namespace Runtime.Entities
{
    public class HighlightableObject : MonoBehaviour
    {
        private Renderer _renderer;
        private Color _originalColor;
        [SerializeField] private Color highlightColor = Color.yellow;

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
    }
}