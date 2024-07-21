using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
     private Dictionary<Vector3Int, PlacementData> placedObjects = new();

     public void AddObjectAt(Vector3Int gridPos, Vector2Int objectSize, int ID, int objectIndex)
     {
          List<Vector3Int> positionToOccupy = CalculatePositions(gridPos,objectSize);
          PlacementData data = new PlacementData(positionToOccupy,ID,objectIndex);
          foreach (var pos in positionToOccupy)
          {
               if (placedObjects.ContainsKey(pos))
               {
                    throw new System.Exception($"Position is already occupied {pos}");
               }
               placedObjects[pos]=data;
          }
     }

     private List<Vector3Int> CalculatePositions(Vector3Int gridPos, Vector2Int objectSize)
     {
          List<Vector3Int> returnVal = new();

          for (int x = 0; x < objectSize.x; x++)
          {
               for (int y = 0; y < objectSize.y; y++)
               {
                    returnVal.Add(gridPos + new Vector3Int(x, 0, y));
               }
          }

          return returnVal;
     }

     public bool CanPlaceObjectAt(Vector3Int gridPos, Vector2Int objectSize)
     {
          List <Vector3Int> positionToOccupy = CalculatePositions(gridPos,objectSize);
          foreach (var pos in positionToOccupy)
          {
               if (placedObjects.ContainsKey(pos))
               {
                    return false;
               }
               
          }
          return true;
     }
}

internal class PlacementData
{
     public List<Vector3Int> occupiedPositions;
     public int ID;
     public int PlacedObjectIndex;
     
     public PlacementData(List<Vector3Int> occupiedPositions,int id, int placedObjectIndex)
     {
          ID = id;
          PlacedObjectIndex = placedObjectIndex;
          this.occupiedPositions = occupiedPositions;
     }
}
