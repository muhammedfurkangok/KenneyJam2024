using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Buildings;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private LayerMask placementLayer, invalidPlacementLayers;
    [SerializeField] private Grid grid;
    private int selectedObjectIndex;
    [SerializeField] private BuildingObjectData buildingObjectData;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private GridData floorData, buildData;
    private Renderer previewRenderer;
    private List<GameObject> placedObjects = new();
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        buildData = new GridData();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition(placementLayer);
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.GetCellCenterWorld(gridPosition);
    }

    [Button]
    private void StartPlacement(int selectedIndex)
    {
        StopPlacement();
        selectedObjectIndex = buildingObjectData.buildingObjectInfoDatas.FindIndex(data => data.BuildingID == selectedIndex);

        if (selectedObjectIndex < 0)
        {
            Debug.LogError("No building object found with type: " + selectedIndex);
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

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        if (placementValidity == false)
        {
            return;
        }

        GameObject newObject = Instantiate(buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].prefab);
        newObject.transform.position = grid.GetCellCenterWorld(gridPosition);
        placedObjects.Add(newObject);
        GridData selectedData = buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].BuildingID == 0 ? floorData : buildData;

        selectedData.AddObjectAt(gridPosition,
            buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].size,
            buildingObjectData.buildingObjectInfoDatas[selectedObjectIndex].BuildingID,
            placedObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int i)
    {
        GridData selectedData = buildingObjectData.buildingObjectInfoDatas[i].BuildingID == 0 ? floorData : buildData;

        if (!selectedData.CanPlaceObjectAt(gridPosition, buildingObjectData.buildingObjectInfoDatas[i].size))
        {
            return false;
        }

        // Ray atımı ile geçerliliği kontrol et
        Vector3 cellCenterWorldPosition = grid.GetCellCenterWorld(gridPosition);
        Ray ray = new Ray(cellCenterWorldPosition + Vector3.up * 10, Vector3.down); // Yukarıdan aşağıya doğru bir ray
        if (Physics.Raycast(ray, 20f, invalidPlacementLayers))
        {
            return false;
        }

        // NavMesh üzerinde geçerliliği kontrol et
        Vector2Int objectSize = buildingObjectData.buildingObjectInfoDatas[i].size;
        Vector3[] checkPoints = GetCheckPoints(cellCenterWorldPosition, objectSize);

        foreach (var point in checkPoints)
        {
            NavMeshHit hit;
            if (!NavMesh.SamplePosition(point, out hit, 1.0f, NavMesh.AllAreas))
            {
                return false;
            }
        }

        return true;
    }

    private Vector3[] GetCheckPoints(Vector3 center, Vector2Int size)
    {
        List<Vector3> checkPoints = new List<Vector3>();
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
                Vector3 offset = new Vector3(x - size.x / 2.0f, 0, z - size.y / 2.0f);
                checkPoints.Add(center + offset);
            }
        }
        return checkPoints.ToArray();
    }
}
