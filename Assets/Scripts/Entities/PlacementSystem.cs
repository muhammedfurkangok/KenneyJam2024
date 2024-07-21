using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Buildings;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
   [SerializeField]private GameObject mouseIndicator,cellIndicator;
   [SerializeField] private LayerMask placementLayer;

   [SerializeField] private Grid grid;
   
   private int selectedObjectIndex;

   [SerializeField] private BuildingObjectData buildingObjectData;
   
   [SerializeField] private GameObject gridVisualization;

   [SerializeField] private GridData floorData, buildData;
   
   private Renderer previewRenderer;
   
   private List<GameObject> placedObjects = new();

   private void Start()
   {
      StopPlacement();
       floorData = new();
         buildData = new();
         previewRenderer= cellIndicator.GetComponentInChildren<Renderer>();
   }

   private void Update()
   {
      if (selectedObjectIndex < 0)
      {
         return;
      }
      Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition(placementLayer);
      Vector3Int gridPosition = grid.WorldToCell(mousePosition);
      
      bool placementValidity = CheckPlacementValidity(gridPosition,selectedObjectIndex);
      previewRenderer.material.color = placementValidity ? Color.white : Color.red;
      
      
      mouseIndicator.transform.position = mousePosition;
      cellIndicator.transform.position = grid.GetCellCenterWorld(gridPosition);
   }
   
   [Button]
   private void StartPlacement(BuildingType selectedtype)
   {
         StopPlacement();
         
         selectedObjectIndex = buildingObjectData.buildingObjectInfoDatas.FindIndex(data => data.type == selectedtype);

         if (selectedObjectIndex < 0)
         {
            Debug.LogError("No building object found with type: " + selectedtype);
            return;
         }
         gridVisualization.SetActive(true);
         cellIndicator.SetActive(true);
         
         InputManager.Instance.OnGridClick += PlaceBuilding;
         InputManager.Instance.OnGridExit += StopPlacement;
   }
   [Button] 
   private void StopPlacement()
   {
      selectedObjectIndex = -1;
      gridVisualization.SetActive(false);
      cellIndicator.SetActive(false);
      InputManager.Instance.OnGridClick -= PlaceBuilding;
      InputManager.Instance.OnGridExit -= StopPlacement;
   }


   private void PlaceBuilding()
   {
      if (InputManager.Instance.IsPointerOverUI())
      {
         return;
      }
      
      Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition(placementLayer);
      Vector3Int gridPosition = grid.WorldToCell(mousePosition);
      
      bool placementValidity = CheckPlacementValidity(gridPosition,selectedObjectIndex);

      if (placementValidity == false)
      {
         return;
      }
      
      GameObject newobject = buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].prefab;
      newobject.transform.position = grid.GetCellCenterWorld(gridPosition);
      placedObjects.Add(newobject);
      GridData selectedData =  buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].BuildingID == 0 ? floorData : buildData;
      
      selectedData.AddObjectAt( gridPosition, 
         buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].size
         , buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].BuildingID, 
         placedObjects.Count - 1);
     
      
   }

   private bool CheckPlacementValidity(Vector3Int gridPosition, int i)
   {
      GridData selectedData =  buildingObjectData.buildingObjectInfoDatas[i].BuildingID == 0 ? floorData : buildData;
      
      return selectedData.CanPlaceObjectAt(gridPosition, buildingObjectData.buildingObjectInfoDatas[i].size);
   }
}
