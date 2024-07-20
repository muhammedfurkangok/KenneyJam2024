using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
   [SerializeField]private GameObject mouseIndicator,cellIndicator;
   [SerializeField] private LayerMask placementLayer;

   [SerializeField] private Grid grid;

   [SerializeField] private GameObject gridVisualization;


   private void Update()
   {
      Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition(placementLayer);
      Vector3Int gridPosition = grid.WorldToCell(mousePosition);
      mouseIndicator.transform.position = mousePosition;
      cellIndicator.transform.position = grid.GetCellCenterWorld(gridPosition);
   }
}
