using UnityEngine;

namespace Entities
{
    public class Resource : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private ResourceType resourceType;
        public int resourceAmount;

        public ResourceType GetResourceType() => resourceType;

        public void MineResource(int amount)
        {
            //Debug.Log("Mining resource: " + resourceType + " amount: " + amount, gameObject);

            resourceAmount -= amount;
            PlayMineAnimation();

            if (resourceAmount <= 0) PlayDestroyAnimation();
        }

        private void PlayMineAnimation()
        {
            // Play animation
        }

        private void PlayDestroyAnimation()
        {
            // Play animation
        }
    }
}