using Data.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class Highlightable : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EntityParameters entityParameters;
        [SerializeField] private Renderer renderer;
        [SerializeField] private int highlightedMaterialIndex;

        private Color originalColor;

        private void Awake()
        {
            originalColor = renderer.materials[highlightedMaterialIndex].color;
        }

        public void Highlight()
        {
            var materials = renderer.materials;
            materials[highlightedMaterialIndex].DOColor(entityParameters.highlightColor, entityParameters.colorChangeDuration);
            renderer.materials = materials;
        }

        public void RemoveHighlight()
        {
            var materials = renderer.materials;
            materials[highlightedMaterialIndex].DOColor(originalColor, entityParameters.colorChangeDuration);
            renderer.materials = materials;
        }
    }
}