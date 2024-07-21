using System;

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

#endregion