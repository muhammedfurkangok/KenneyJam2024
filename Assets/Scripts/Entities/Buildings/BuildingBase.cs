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
        [SerializeField] private ResourceAndAmount[] maintainCost;
        [SerializeField] private ResourceAndAmount[] yield;

        public ResourceAndAmount[] GetMaintainCost() => maintainCost;
        public ResourceAndAmount[] GetYield() => yield;
        public BuildingType GetBuildingType() => type;

        public override void Upgrade()
        {
            base.Upgrade();

            ResourceManager.Instance.DecreaseMaintainCost(maintainCost);
            ResourceManager.Instance.DecreaseYield(yield);

            maintainCost = buildingData.GetMaintainCostArray(type, tier);
            yield = buildingData.GetYield(type, tier);

            ResourceManager.Instance.IncreaseMaintainCost(maintainCost);
            ResourceManager.Instance.IncreaseYield(yield);

            foreach (var resource in buildingData.GetBuildCostArray(type, tier)) ResourceManager.Instance.DecreaseResource(resource.resource, resource.amount);
        }

        protected virtual void Start()
        {
            TimeManager.Instance.OnTimeCycleCompleted += OnTimeCycleCompleted;

            maintainCost = buildingData.GetMaintainCostArray(type, tier);
            yield = buildingData.GetYield(type, tier);

            ResourceManager.Instance.IncreaseMaintainCost(maintainCost);
            ResourceManager.Instance.IncreaseYield(yield);
        }

        protected virtual void OnDisable()
        {
            TimeManager.Instance.OnTimeCycleCompleted -= OnTimeCycleCompleted;
        }

        private void OnMouseDown()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.Free) UIManager.Instance.OpenBuildingUI(this);
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
        
    }
}