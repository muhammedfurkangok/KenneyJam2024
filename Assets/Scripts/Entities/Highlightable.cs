using Data.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class Highlightable : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EntityParameters entityParameters;
        [SerializeField] private RendererAndMaterialIndex[] renderersAndMaterialIndices;

        private Color[] originalColors;

        private void Start()
        {
            originalColors = new Color[renderersAndMaterialIndices.Length];
            for (var i = 0; i < renderersAndMaterialIndices.Length; i++)
            {
                originalColors[i] = renderersAndMaterialIndices[i].renderer.materials[renderersAndMaterialIndices[i].materialIndex].color;
            }
        }

        public void Highlight()
        {
            for (var i = 0; i < renderersAndMaterialIndices.Length; i++)
            {
                var materials = renderersAndMaterialIndices[i].renderer.materials;
                materials[renderersAndMaterialIndices[i].materialIndex].DOColor(entityParameters.highlightColor, entityParameters.colorChangeDuration);
                renderersAndMaterialIndices[i].renderer.materials = materials;
            }
        }

        public void RemoveHighlight()
        {
            for (var i = 0; i < renderersAndMaterialIndices.Length; i++)
            {
                var materials = renderersAndMaterialIndices[i].renderer.materials;
                materials[renderersAndMaterialIndices[i].materialIndex].DOColor(originalColors[i], entityParameters.colorChangeDuration);
                renderersAndMaterialIndices[i].renderer.materials = materials;
            }
        }
    }
}