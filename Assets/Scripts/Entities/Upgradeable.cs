using UnityEngine;

namespace Entities
{
    public class Upgradeable : MonoBehaviour
    {
        [Header("Upgradeable - References")]
        [SerializeField] private RendererMaterialIndexAndColorArray[] renderersMaterialIndicesAndColorArrays;
        [SerializeField] private Highlightable highlightable;

        [Header("Upgradeable - Info - No Touch")]
        [SerializeField] protected int tier;

        public int GetTier() => tier;

        public virtual void Upgrade()
        {
            tier++;
            ChangeColors();
        }

        private void ChangeColors()
        {
            highlightable.RemoveHighlight();

            var rendererMaterialIndicesAndColors = renderersMaterialIndicesAndColorArrays[tier];
            foreach (var rendererMaterialIndexAndColor in rendererMaterialIndicesAndColors.rendererMaterialIndicesAndColors)
            {
                var renderer = rendererMaterialIndexAndColor.renderer;
                var materialIndex = rendererMaterialIndexAndColor.materialIndex;
                var color = rendererMaterialIndexAndColor.color;

                renderer.materials[materialIndex].color = color;
            }
        }
    }
}