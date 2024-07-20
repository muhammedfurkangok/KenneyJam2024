using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    public Texture2D fogTexture;
    public float revealRadius = 5.0f;
    public LayerMask fogLayer;

    private Color[] fogColors;
    private int textureWidth;
    private int textureHeight;
    private Vector3 mapSize;

    void Start()
    {
        InitializeFog();
    }

    void InitializeFog()
    {
        textureWidth = fogTexture.width;
        textureHeight = fogTexture.height;
        fogColors = new Color[textureWidth * textureHeight];

        for (int i = 0; i < fogColors.Length; i++)
        {
            fogColors[i] = Color.black; 
        }

        fogTexture.SetPixels(fogColors);
        fogTexture.Apply();
    }

    public void RevealArea(Vector3 position, float range)
    {
        Vector3 relativePos = position - transform.position;
        int centerX = Mathf.FloorToInt(relativePos.x / mapSize.x * textureWidth);
        int centerY = Mathf.FloorToInt(relativePos.z / mapSize.z * textureHeight);
        int revealRadius = Mathf.FloorToInt(range / mapSize.x * textureWidth);

        for (int x = -revealRadius; x <= revealRadius; x++)
        {
            for (int y = -revealRadius; y <= revealRadius; y++)
            {
                int px = centerX + x;
                int py = centerY + y;

                if (px >= 0 && px < textureWidth && py >= 0 && py < textureHeight)
                {
                    float distance = Mathf.Sqrt(x * x + y * y);
                    if (distance <= revealRadius)
                    {
                        int index = px + py * textureWidth;
                        fogColors[index] = Color.clear;
                    }
                }
            }
        }

        fogTexture.SetPixels(fogColors);
        fogTexture.Apply();
    }
}