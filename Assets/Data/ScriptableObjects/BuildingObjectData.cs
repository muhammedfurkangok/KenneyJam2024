using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Buildings;
using UnityEngine;

[CreateAssetMenu]
public class BuildingObjectData : ScriptableObject
{
   public List<BuildingObjectInfoData> buildingObjectInfoDatas;
}

[Serializable]
public struct BuildingObjectInfoData
{
   public string Name;
   public int BuildingID;
   public BuildingType type;
   public Vector2Int size;
   public GameObject prefab;
   
}
