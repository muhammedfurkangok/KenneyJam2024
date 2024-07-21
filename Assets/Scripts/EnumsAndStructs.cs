using System;
using UnityEngine;

#region Enums

public enum ResourceType
{
    Population,
    Energy,
    Food,
    Money,
    Metal,
    MetalPremium,
    Gem
}

public enum BuildingType
{
    HQ,
    LivingSpace,
    MinerBuilding,
    ScoutBuilding,
    RocketSite
}

public enum VehicleType
{
    Miner,
    Scout,
}

public enum SoundType
{
    ButtonClick,
}

#endregion

#region Structs

[Serializable]
public struct Resource
{
    public ResourceType type;
    public int amount;
}

[Serializable]
public struct BuildingCostAndYieldData
{
    public BuildingType type;
    public int tier;
    public BuildingResourceInfo[] buildCost;
    public BuildingResourceInfo[] maintainCost;
    public BuildingResourceInfo[] yield;
    public BuildingResourceInfo[] singleTimeYield;
}

[Serializable]
public struct BuildingResourceInfo
{
    public ResourceType resource;
    public int amount;
}

[Serializable]
public struct RendererMaterialIndexAndColorArray
{
    public RendererMaterialIndexAndColor[] rendererMaterialIndicesAndColors;
}

[Serializable]
public struct RendererMaterialIndexAndColor
{
    public Renderer renderer;
    public int materialIndex;
    public Color color;
}

[Serializable]
public struct RendererAndMaterialIndex
{
    public Renderer renderer;
    public int materialIndex;
}

[Serializable]
public struct VehicleSpeedData
{
    public VehicleType type;
    public int tier;
    public SpeedInfo speedInfo;
}

[Serializable]
public struct SpeedInfo
{
    public float speed;
    public float angularSpeed;
    public float acceleration;
}

#endregion