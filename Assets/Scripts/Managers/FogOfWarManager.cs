using UnityEngine;

namespace Managers
{
    public class FogOfWarManager : MonoBehaviour
    {
        public int textureSize = 256;
        public float worldSize = 50.0f;
        public Color fogColor = Color.black;
        public Color revealColor = Color.white;

        private Texture2D fogTexture;
        private Color[] fogColors;
        private MeshRenderer fogRenderer;

        private void Start()
        {
            fogRenderer = GetComponent<MeshRenderer>();
            InitializeFogTexture();
        }

        private void InitializeFogTexture()
        {
            fogTexture = new Texture2D(textureSize, textureSize);
            fogColors = new Color[textureSize * textureSize];
            for (int i = 0; i < fogColors.Length; i++)
            {
                fogColors[i] = fogColor;
            }
            fogTexture.SetPixels(fogColors);
            fogTexture.Apply();
            fogRenderer.material.mainTexture = fogTexture;
        }

        public void RevealArea(Vector3 position, float range)
        {
            Vector2 textureCoord = WorldToTextureCoord(position);
            int revealRange = Mathf.FloorToInt(range / worldSize * textureSize);

            for (int x = -revealRange; x <= revealRange; x++)
            {
                for (int y = -revealRange; y <= revealRange; y++)
                {
                    int texX = Mathf.FloorToInt(textureCoord.x) + x;
                    int texY = Mathf.FloorToInt(textureCoord.y) + y;

                    if (texX >= 0 && texX < textureSize && texY >= 0 && texY < textureSize)
                    {
                        float distance = Vector2.Distance(textureCoord, new Vector2(texX, texY));
                        if (distance <= revealRange)
                        {
                            fogColors[texY * textureSize + texX] = revealColor; // Sisli alanı aç
                        }
                    }
                }
            }
            fogTexture.SetPixels(fogColors);
            fogTexture.Apply();

            // Mesh'in görünürlük durumunu kontrol et
            if (fogRenderer != null)
            {
                // Mesh'in görünürlük durumunu değiştirme
                fogRenderer.enabled = true;
            }
        }

        private Vector2 WorldToTextureCoord(Vector3 worldPos)
        {
            float relativeX = (worldPos.x / worldSize) + 0.5f;
            float relativeZ = (worldPos.z / worldSize) + 0.5f;
            int texX = Mathf.FloorToInt(relativeX * textureSize);
            int texY = Mathf.FloorToInt(relativeZ * textureSize);
            return new Vector2(texX, texY);
        }
    }
}
