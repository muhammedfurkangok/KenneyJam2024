using System;
using System.Linq;
using Data.ScriptableObjects;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : SingletonMonoBehaviour<ResourceManager>
    {
        [Header("References")]
        [SerializeField] private ResourceData resourceData;

        [Header("Parameters")]
        [SerializeField] private ResourceAndAmount[] resourceStartAmounts;
        [SerializeField] private ResourceType[] lethalResources;
        [SerializeField] private int criticalResourceCycleThreshold;

        [Header("Info - No Touch")]
        [SerializeField] private Resource[] resources;
        [SerializeField] private ResourceAndAmount[] currentMaintainCosts;
        [SerializeField] private ResourceAndAmount[] currentYields;

        protected override void Awake()
        {
            base.Awake();

            //Init currentMaintainCosts
            currentMaintainCosts = new ResourceAndAmount[Enum.GetNames(typeof(ResourceType)).Length];
            for (var i = 0; i < currentMaintainCosts.Length; i++)
            {
                currentMaintainCosts[i].resource = (ResourceType)i;
                currentMaintainCosts[i].amount = 0;
            }

            //Init currentYields
            currentYields = new ResourceAndAmount[Enum.GetNames(typeof(ResourceType)).Length];
            for (var i = 0; i < currentYields.Length; i++)
            {
                currentYields[i].resource = (ResourceType)i;
                currentYields[i].amount = 0;
            }
        }

        private void Start()
        {
            resources = new Resource[Enum.GetNames(typeof(ResourceType)).Length];

            for (var i = 0; i < resources.Length; i++)
            {
                resources[i].type = (ResourceType)i;
                resources[i].amount = resourceStartAmounts[i].amount;
            }

            UIManager.Instance.RefreshResourceUI();
        }

        public int GetResourceAmount(ResourceType type)
        {
            return (from resource in resources where resource.type == type select resource.amount).FirstOrDefault();
        }

        public void IncreaseResource(ResourceType type, int amount)
        {
            for (var i = 0; i < resources.Length; i++)
            {
                if (resources[i].type == type)
                {
                    resources[i].amount += amount;
                    //Debug.Log("Increased " + type + " by " + amount);
                    break;
                }
            }

            UIManager.Instance.RefreshResourceUI();
        }

        public void DecreaseResource(ResourceType type, int amount)
        {
            for (var i = 0; i < resources.Length; i++)
            {
                if (resources[i].type == type)
                {
                    resources[i].amount -= amount;
                    //Debug.Log("Decreased " + type + " by " + amount);

                    if (resources[i].amount <= 0 && lethalResources.Contains(resources[i].type))
                    {
                        GameManager.Instance.FailGame(resources[i].type.ToString());
                    }

                    break;
                }
            }

            UIManager.Instance.RefreshResourceUI();
        }

        public void IncreaseMaintainCost(ResourceAndAmount[] maintainCost)
        {
            foreach (var resource in maintainCost)
            {
                for (var i = 0; i < currentMaintainCosts.Length; i++)
                {
                    if (currentMaintainCosts[i].resource == resource.resource)
                    {
                        currentMaintainCosts[i].amount += resource.amount;
                        break;
                    }
                }
            }
        }

        public void DecreaseMaintainCost(ResourceAndAmount[] maintainCost)
        {
            foreach (var resource in maintainCost)
            {
                for (var i = 0; i < currentMaintainCosts.Length; i++)
                {
                    if (currentMaintainCosts[i].resource == resource.resource)
                    {
                        currentMaintainCosts[i].amount -= resource.amount;
                        break;
                    }
                }
            }
        }

        public void IncreaseYield(ResourceAndAmount[] yield)
        {
            foreach (var resource in yield)
            {
                for (var i = 0; i < currentYields.Length; i++)
                {
                    if (currentYields[i].resource == resource.resource)
                    {
                        currentYields[i].amount += resource.amount;
                        break;
                    }
                }
            }
        }

        public void DecreaseYield(ResourceAndAmount[] yield)
        {
            foreach (var resource in yield)
            {
                for (var i = 0; i < currentYields.Length; i++)
                {
                    if (currentYields[i].resource == resource.resource)
                    {
                        currentYields[i].amount -= resource.amount;
                        break;
                    }
                }
            }
        }

        public bool TryBuyResource(ResourceType type, int amount)
        {
            var money = GetResourceAmount(ResourceType.Money);
            var cost = resourceData.GetResourceMoneyValue(type) * amount;

            if (type == ResourceType.Population)
            {
                var currentPopulation = GetResourceAmount(ResourceType.Population);
                var currentCapacity = GetResourceAmount(ResourceType.PopulationCapacity);
                var remainingCapacity = currentCapacity - currentPopulation;
                if (remainingCapacity < amount) return false;
            }

            if (money >= cost)
            {
                DecreaseResource(ResourceType.Money, cost);
                IncreaseResource(type, amount);
                return true;
            }

            return false;
        }

        public bool TrySellResource(ResourceType type, int amount)
        {
            var cost = resourceData.GetResourceMoneyValue(type) * amount;

            if (GetResourceAmount(type) >= amount)
            {
                IncreaseResource(ResourceType.Money, cost);
                DecreaseResource(type, amount);
                return true;
            }

            return false;
        }

        public int GetMaintainCost(ResourceType type)
        {
            return (from resource in currentMaintainCosts where resource.resource == type select resource.amount).FirstOrDefault();
        }

        public int GetYield(ResourceType type)
        {
            return (from resource in currentYields where resource.resource == type select resource.amount).FirstOrDefault();
        }

        public int GetResourceConsumeAmount(ResourceType type)
        {
            return GetMaintainCost(type) - GetYield(type);
        }

        public bool IsResourceCritical(ResourceType type)
        {
            if (GetMaintainCost(type) == 0) return false; //If the resource is not consumed, it can't be critical, also prevents division by zero
            var remainingCycles = GetResourceAmount(type) / GetMaintainCost(type) - GetYield(type);
            if (remainingCycles < 0) return false; //If the resource is produced more than consumed, it can't be critical
            return remainingCycles <= criticalResourceCycleThreshold;
        }
    }
}