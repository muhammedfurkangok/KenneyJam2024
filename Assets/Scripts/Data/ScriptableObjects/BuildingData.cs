using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingData : ScriptableObject
{
    public List<GameObject> buildingPrefabs;
    
}
[Serializable]
public struct Building
{
    public string name;
    public int ID;
    public Vector2Int size;
    public GameObject prefab;
}
