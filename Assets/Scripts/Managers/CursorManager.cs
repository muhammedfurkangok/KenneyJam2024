using System;
using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        public Texture2D normalCursor;       
        public Texture2D selectableCursor;   
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }

        private void Update()
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Selectable")) 
                {
                    Cursor.SetCursor(selectableCursor, Vector2.zero, CursorMode.Auto); 
                }
                else
                {
                    Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto); 
                }
            }
            else
            {
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}