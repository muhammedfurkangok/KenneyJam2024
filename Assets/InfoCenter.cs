using Entities.Buildings;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCenter : MonoBehaviour
{

    private BuildingBase[] buildings;

    private float cycleDuration;

    private void Start()
    {
        cycleDuration = TimeManager.Instance.GetCycleDuration();
        GameObject[] g = GameObject.FindGameObjectsWithTag("Building");

        for (int i = 0; i < g.Length; i++)
        {
            buildings[i] = g[i].GetComponent<BuildingBase>();
        }
    }

    public float GetResourseDepletionTime(ResourceType type)
    {
        float resourceAmount = ResourceManager.Instance.GetResourceAmount(type);
        return CalculateResourceDepletionTime(type,resourceAmount);
    }

    float CalculateResourceDepletionTime(ResourceType type,float resourceAmount)
    {
        //resource depletion time = resource amount / resource consumption rate
        float resourceConsumptionRate = CalculateResourceConsumptionRate(type);
        return resourceAmount / resourceConsumptionRate;
    }
    float CalculateResourceConsumptionRate(ResourceType type)
    {
        //resource consumption rate =
        //(resource consuming buildings * maintain costs - resource yielding buildings * yield )/ cycle duration
        float maintainCosts = 0;
        float yields = 0;
        float resourceConsumptionRate = 0;
        foreach (BuildingBase building in buildings)
        {
            if (building.GetMaintainCost() != null)
            {
                foreach (BuildingResourceInfo resource in building.GetMaintainCost())
                {
                    if (resource.resource == type)
                    {
                        maintainCosts += resource.amount;
                    }
                }
            }
            if (building.GetYield() != null)
            {
                foreach (BuildingResourceInfo resource in building.GetYield())
                {
                    if (resource.resource == type)
                    {
                        yields += resource.amount;
                    }
                }
            }
        }

        return resourceConsumptionRate = (maintainCosts - yields) / cycleDuration;
    }
}
