using System;
using Data.ScriptableObjects;
using Managers;
using UnityEngine;

namespace Entities.Buildings
{
    public abstract class BuildingBase : Upgradeable
    {
        [Header("Building Base - References")]
        [SerializeField] private BuildingData buildingData;

        [Header("Building Base - Parameters")]
        [SerializeField] private BuildingType type;

        [Header("Building Base - Info - No Touch")]
        [SerializeField] private BuildingResourceInfo[] maintainCost;
        [SerializeField] private BuildingResourceInfo[] yield;

        public BuildingType GetBuildingType() => type;

        public override void Upgrade()
        {
            base.Upgrade();

            maintainCost = buildingData.GetMaintainCost(type, tier);
            yield = buildingData.GetYield(type, tier);

            DecreaseUpdateCost();
        }

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

        private void OnMouseDown()
        {
            UIManager.Instance.OpenBuildingUI(this);
        }

        protected virtual void OnTimeCycleCompleted()
        {
            foreach (var resource in maintainCost)
            {
                ResourceManager.Instance.DecreaseResource(resource.resource, resource.amount);
            }

            foreach (var resource in yield)
            {
                ResourceManager.Instance.IncreaseResource(resource.resource, resource.amount);
            }
        }

        private void DecreaseUpdateCost()
        {
            foreach (var resource in buildingData.GetBuildCost(type, tier))
            {
                ResourceManager.Instance.DecreaseResource(resource.resource, resource.amount);
            }
        }
    }
}