using System;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : SingletonMonoBehaviour<ResourceManager>
    {
        [Header("Parameters")]
        [SerializeField] private ResourceAndAmount[] resourceStartAmounts;
        [SerializeField] private ResourceType[] lethalResources;
        [SerializeField] private int criticalResourceCycleThreshold;

        [Header("Info - No Touch")]
        [SerializeField] private Resource[] resources;
        [SerializeField] private ResourceAndAmount[] currentMaintainCosts;

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
            foreach (var resource in resources)
            {
                if (resource.type == type)
                {
                    return resource.amount;
                }
            }

            return 0;
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

        public int GetMaintainCost(ResourceType type)
        {
            foreach (var resource in currentMaintainCosts)
            {
                if (resource.resource == type) return resource.amount;
            }

            return 0;
        }

        public bool IsResourceCritical(ResourceType type)
        {
            if (GetMaintainCost(type) == 0) return false; //If the resource is not consumed, it can't be critical, also prevents division by zero
            var remainingCycles = GetResourceAmount(type) / GetMaintainCost(type);
            return remainingCycles <= criticalResourceCycleThreshold;
        }
    }
}