using System;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : SingletonMonoBehaviour<ResourceManager>
    {
        [Header("Parameters")]
        [SerializeField] private BuildingResourceInfo[] resourceStartAmounts;
        [SerializeField] private ResourceType[] lethalResources;

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
    }
}