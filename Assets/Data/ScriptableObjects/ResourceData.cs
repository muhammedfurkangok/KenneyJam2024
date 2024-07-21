using UnityEngine;

namespace Data.ScriptableObjects
{
    //[CreateAssetMenu(fileName = "ResourceData", menuName = "ResourceData", order = 0)]
    public class ResourceData : ScriptableObject
    {
        [Header("Resource Money Data")]
        [SerializeField] private ResourceMoneyData[] resourceMoneyDatas;

        public int GetResourceMoneyValue(ResourceType resourceType)
        {
            foreach (var resourceMoneyData in resourceMoneyDatas)
            {
                if (resourceMoneyData.type == resourceType)
                {
                    return resourceMoneyData.moneyValue;
                }
            }

            throw new System.Exception("Resource money amount not found for: " + resourceType);
        }
    }
}