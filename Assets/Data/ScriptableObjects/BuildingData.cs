using System;
using UnityEngine;

namespace Data.ScriptableObjects
{
    //[CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingData", order = 0)]
    public class BuildingData : ScriptableObject
    {
        [Header("Building Costs")]
        [SerializeField] private BuildingCostAndYieldData[] buildingCostAndYieldDatas;

        public ResourceAndAmount[] GetBuildCostArray(BuildingType buildingType, int buildingTier)
        {
            foreach (var buildingCostAndYieldData in buildingCostAndYieldDatas)
            {
                if (buildingCostAndYieldData.type == buildingType && buildingCostAndYieldData.tier == buildingTier)
                {
                    return buildingCostAndYieldData.buildCost;
                }
            }

            throw new Exception("Building cost not found for: " + buildingType + " " + buildingTier);
        }

        public int GetBuildCost(BuildingType buildingType, int buildingTier, ResourceType resourceType)
        {
            foreach (var buildingCostAndYieldData in buildingCostAndYieldDatas)
            {
                if (buildingCostAndYieldData.type == buildingType && buildingCostAndYieldData.tier == buildingTier)
                {
                    foreach (var resourceAndAmount in buildingCostAndYieldData.buildCost)
                    {
                        if (resourceAndAmount.resource == resourceType)
                        {
                            return resourceAndAmount.amount;
                        }
                    }
                }
            }

            return 0;
        }

        public ResourceAndAmount[] GetMaintainCostArray(BuildingType buildingType, int buildingTier)
        {
            foreach (var buildingCostData in buildingCostAndYieldDatas)
            {
                if (buildingCostData.type == buildingType && buildingCostData.tier == buildingTier)
                {
                    return buildingCostData.maintainCost;
                }
            }

            throw new Exception("Building maintain cost not found for: " + buildingType + " " + buildingTier);
        }

        public int GetMaintainCost(BuildingType buildingType, int buildingTier, ResourceType resourceType)
        {
            foreach (var buildingCostAndYieldData in buildingCostAndYieldDatas)
            {
                if (buildingCostAndYieldData.type == buildingType && buildingCostAndYieldData.tier == buildingTier)
                {
                    foreach (var resourceAndAmount in buildingCostAndYieldData.maintainCost)
                    {
                        if (resourceAndAmount.resource == resourceType)
                        {
                            return resourceAndAmount.amount;
                        }
                    }
                }
            }

            return 0;
        }

        public ResourceAndAmount[] GetYield(BuildingType buildingType, int buildingTier)
        {
            foreach (var buildingCostData in buildingCostAndYieldDatas)
            {
                if (buildingCostData.type == buildingType && buildingCostData.tier == buildingTier)
                {
                    return buildingCostData.yield;
                }
            }

            throw new Exception("Building yield not found for: " + buildingType + " " + buildingTier);
        }

        public ResourceAndAmount[] GetSingleTimeYield(BuildingType buildingType, int buildingTier)
        {
            foreach (var buildingCostData in buildingCostAndYieldDatas)
            {
                if (buildingCostData.type == buildingType && buildingCostData.tier == buildingTier)
                {
                    return buildingCostData.singleTimeYield;
                }
            }

            throw new Exception("Building single time yield not found for: " + buildingType + " " + buildingTier);
        }
    }
}