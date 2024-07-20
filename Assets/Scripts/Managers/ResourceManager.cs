using System;
using UnityEngine;

namespace Managers
{
    public enum ResourceType
    {
        Population,
        Energy,
        Food,
        Money,
        Metal,
        MetalPremium,
        Gem
    }

    [Serializable]
    public struct Resource
    {
        public ResourceType type;
        public int amount;
    }

    public class ResourceManager : MonoBehaviour
    {
        [Header("Info - No Touch")]
        [SerializeField] private Resource[] resources;

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