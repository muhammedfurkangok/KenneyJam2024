using DG.Tweening;
using Sirenix.OdinInspector;
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

        [Button]
        private void PlayMineAnimation()
        {
            //Sound
            
            transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1).SetEase(Ease.OutBounce);
            
            //Particle istersek
        }

        [Button]
        private void PlayDestroyAnimation()
        {
            //Sound
            
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
            
            //Particle istersek
        }
    }
}