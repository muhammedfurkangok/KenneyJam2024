using System;
using Runtime.Extensions;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public struct Resource
    {
        public ResourceType type;
        public int amount;
    }

    [Serializable]
    public struct ResourceStartAmount
    {
        public ResourceType type;
        public int amount;
    }

    public class ResourceManager : SingletonMonoBehaviour<ResourceManager>
    {
        [Header("Parameters")]
        [SerializeField] private ResourceStartAmount[] resourceStartAmounts;

        [Header("Info - No Touch")]
        [SerializeField] private Resource[] resources;

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
                    break;
                }
            }
        }

        public void DecreaseResource(ResourceType type, int amount)
        {
            for (var i = 0; i < resources.Length; i++)
            {
                if (resources[i].type == type)
                {
                    resources[i].amount -= amount;
                    break;
                }
            }
        }
    }
}