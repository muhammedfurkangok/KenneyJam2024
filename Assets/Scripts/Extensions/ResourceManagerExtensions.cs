using Entities.Buildings;
using Managers;
using System;
using UnityEngine;

namespace Extensions
{
    public static class ResourceManagerExtensions
    {
        public static float GetResourceDepletionTime(this ResourceManager rm,ResourceType type)
        {
            if (type == ResourceType.Energy || type == ResourceType.Food)
            {
                float resourceAmount = rm.GetResourceAmount(type);
                return CalculateResourceDepletionTime(type, resourceAmount);
            }
            else
            {
                throw new Exception("Resource type is not valid, type must be Energy or Food");
            }
        }
        public static float GetResourceConsumptionRate(this ResourceManager rm,ResourceType type)
        {
            if (type == ResourceType.Energy || type == ResourceType.Food)
            {
                return CalculateResourceConsumptionRate(type);
            }
            else throw new Exception("Resource type is not valid, type must be Energy or Food");
        }

        public static float GetPopulationCapacity(this ResourceManager rm)
        {
            //mesela
            //return rm.GetResourceAmount(ResourceType.Population);
            return 0;
        }

        static float CalculateResourceDepletionTime(ResourceType type, float resourceAmount)
        {
            //resource depletion time = resource amount / resource consumption rate

            float resourceConsumptionRate = CalculateResourceConsumptionRate(type);

            return resourceAmount / resourceConsumptionRate;
        }
        static float CalculateResourceConsumptionRate(ResourceType type)
        {
            //resource consumption rate =
            //(resource consuming buildings * maintain costs - resource yielding buildings * yield )/ cycle duration
            float cycleDuration = TimeManager.Instance.GetCycleDuration();
            GameObject[] g = GameObject.FindGameObjectsWithTag("Building");
            BuildingBase[] buildings = new BuildingBase[g.Length];

            for (int i = 0; i < g.Length; i++)
            {
                buildings[i] = g[i].GetComponent<BuildingBase>();
            }

            float maintainCosts = 0;
            float yields = 0;

            foreach (BuildingBase building in buildings)
            {
                if (building.MaintainCost != null)
                {
                    foreach (BuildingResourceInfo resource in building.MaintainCost)
                    {
                        if (resource.resource == type)
                        {
                            maintainCosts += resource.amount;
                        }
                    }
                }
                if (building.Yield != null)
                {
                    foreach (BuildingResourceInfo resource in building.Yield)
                    {
                        if (resource.resource == type)
                        {
                            yields += resource.amount;
                        }
                    }
                }
            }

            return (maintainCosts - yields) / cycleDuration;
        }
    }
}