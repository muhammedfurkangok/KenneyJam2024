using Managers;
using UnityEngine;

namespace Entities.Buildings
{
    public enum BuildingType
    {
        HQ,
        LivingSpace,
        MinerBuilding,
        ScoutBuilding,
        RocketSite
    }

    public abstract class BuildingBase : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BuildingType type;
        [SerializeField] private Renderer renderer;
        [SerializeField] private Material[] tier2Materials;
        [SerializeField] private Material[] tier3Materials;

        [Header("Info - No Touch")]
        [SerializeField] private int tier = 1;

        protected virtual void Start()
        {
            TimeManager.Instance.OnTimeCycleCompleted += OnTimeCycleCompleted;
        }

        protected virtual void OnDisable()
        {
            TimeManager.Instance.OnTimeCycleCompleted -= OnTimeCycleCompleted;
        }

        protected virtual void OnTimeCycleCompleted()
        {

        }

        public void UpgradeBuilding()
        {
            tier++;

            if (tier == 2) renderer.materials = tier2Materials;
            else if (tier == 3) renderer.materials = tier3Materials;
        }
    }
}