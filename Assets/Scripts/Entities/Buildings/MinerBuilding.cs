using Entities.Vehicles;
using UnityEngine;

namespace Entities.Buildings
{
    public class MinerBuilding : BuildingBase
    {
        [Header("Miner Building - References")]
        [SerializeField] private MinerVehicle minerVehicle;

        public override void Upgrade()
        {
            base.Upgrade();

            minerVehicle.Upgrade();
        }
    }
}