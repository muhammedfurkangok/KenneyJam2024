using System;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [Serializable]
    public struct BuildingCostData
    {
        public BuildingType type;
        public int tier;
        public BuildingBuildCost buildBuildCost;
        public BuildingMaintainCost maintainCost;
        public BuildingYield yield;
    }

    [Serializable]
    public struct BuildingBuildCost
    {
        public ResourceType[] resources;
        public int[] amounts;
        public int buildDurationAsTimeCycle;
    }

    [Serializable]
    public struct BuildingMaintainCost
    {
        public ResourceType[] resources;
        public int[] amounts;
    }

    [Serializable]
    public struct BuildingYield
    {
        public ResourceType[] resources;
        public int[] amounts;
    }

    //[CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingData", order = 0)]
    public class BuildingData : ScriptableObject
    {
        [Header("Building Costs")]
        [SerializeField] private BuildingCostData[] buildingCostDatas;

        public BuildingBuildCost GetBuildCost(BuildingType buildingType, int buildingTier)
        {
            foreach (var buildingCostData in buildingCostDatas)
            {
                if (buildingCostData.type == buildingType && buildingCostData.tier == buildingTier)
                {
                    return buildingCostData.buildBuildCost;
                }
            }

            return new BuildingBuildCost();
        }

        public BuildingMaintainCost GetMaintainCost(BuildingType buildingType, int buildingTier)
        {
            foreach (var buildingCostData in buildingCostDatas)
            {
                if (buildingCostData.type == buildingType && buildingCostData.tier == buildingTier)
                {
                    return buildingCostData.maintainCost;
                }
            }

            return new BuildingMaintainCost();
        }

        public BuildingYield GetYield(BuildingType buildingType, int buildingTier)
        {
            foreach (var buildingCostData in buildingCostDatas)
            {
                if (buildingCostData.type == buildingType && buildingCostData.tier == buildingTier)
                {
                    return buildingCostData.yield;
                }
            }

            return new BuildingYield();
        }
    }
}