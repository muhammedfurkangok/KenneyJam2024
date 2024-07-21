using Data.ScriptableObjects;
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
        }

        public void Highlight()
        {
            for (var i = 0; i < renderersAndMaterialIndices.Length; i++)
            {
                var renderer = renderersAndMaterialIndices[i].renderer;
                var materialIndex = renderersAndMaterialIndices[i].materialIndex;

                originalColors[i] = renderer.materials[materialIndex].color;
                renderer.materials[materialIndex].color = entityParameters.highlightColor;
            }
        }

        public void RemoveHighlight()
        {
            for (var i = 0; i < renderersAndMaterialIndices.Length; i++)
            {
                var renderer = renderersAndMaterialIndices[i].renderer;
                var materialIndex = renderersAndMaterialIndices[i].materialIndex;

                renderer.materials[materialIndex].color = originalColors[i];
            }
        }
    }
}