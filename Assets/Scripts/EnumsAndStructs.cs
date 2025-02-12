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
    Gem,
    PopulationCapacity,
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

public enum GameState
{
    Free,
    UI,
    VehicleControl,
    Placement,
}

public enum CursorType
{
    Default,
    Selectable,
    VehicleControl,
    Disabled,
    Mine,
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
    public ResourceAndAmount[] buildCost;
    public ResourceAndAmount[] maintainCost;
    public ResourceAndAmount[] yield;
    public ResourceAndAmount[] singleTimeYield;
}

[Serializable]
public struct ResourceAndAmount
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

[Serializable]
public struct ResourceMoneyData
{
    public ResourceType type;
    public int moneyValue;
}

[Serializable]
public struct CursorInfo
{
    public CursorType type;
    public Texture2D texture;
    public Vector2 hotspot;
}

#endregion