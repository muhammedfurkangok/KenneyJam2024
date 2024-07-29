using UnityEngine;

namespace Entities.Buildings
{
    public class RocketSite : BuildingBase
    {
        [Header("Rocket Info - No Touch")]
        [SerializeField] private int capacity;

        public int GetCapacity() => capacity;
    }
}