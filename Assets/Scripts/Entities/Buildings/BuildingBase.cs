using System;
using Data.ScriptableObjects;
using Managers;
using UnityEngine;

namespace Entities.Buildings
{
    public enum BuildingType
    {
        HQ,
        LivingSpace,
        MinerBuilding,
        ScoutBuilding,
        RocketSite
    }

    public abstract class BuildingBase : MonoBehaviour
    {
        [Header("Building Base - References")]
        [SerializeField] private BuildingData buildingData;
        [SerializeField] private BuildingType type;
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Material[] tier2Materials;
        [SerializeField] private Material[] tier3Materials;

        [Header("Building Base - Info - No Touch")]
        [SerializeField] private int tier = 1;
        [SerializeField] private BuildingMaintainCost maintainCost;
        [SerializeField] private BuildingYield yield;

        protected virtual void Start()
        {
            TimeManager.Instance.OnTimeCycleCompleted += OnTimeCycleCompleted;

            maintainCost = buildingData.GetMaintainCost(type, tier);
            yield = buildingData.GetYield(type, tier);
        }

        protected virtual void OnDisable()
        {
            TimeManager.Instance.OnTimeCycleCompleted -= OnTimeCycleCompleted;
        }

        protected virtual void OnTimeCycleCompleted()
        {
            for (var i = 0; i < maintainCost.resources.Length; i++)
            {
                ResourceManager.Instance.DecreaseResource(maintainCost.resources[i], maintainCost.amounts[i]);
            }

            for (var i = 0; i < yield.resources.Length; i++)
            {
                ResourceManager.Instance.IncreaseResource(yield.resources[i], yield.amounts[i]);
            }
        }

        public virtual void UpgradeBuilding()
        {
            tier++;

            maintainCost = buildingData.GetMaintainCost(type, tier);
            yield = buildingData.GetYield(type, tier);

            foreach (var renderer in renderers)
            {
                if (tier == 2) renderer.materials = tier2Materials;
                else if (tier == 3) renderer.materials = tier3Materials;
            }
        }
    }
}