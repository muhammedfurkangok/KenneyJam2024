using Entities.Vehicles;
using UnityEngine;

namespace Entities.Buildings
{
    public class ScoutBuilding : BuildingBase
    {
        [Header("Scout Building - References")]
        [SerializeField] private ScoutVehicle scoutVehicle;

        public override void Upgrade()
        {
            base.Upgrade();

            scoutVehicle.Upgrade();
        }
    }
}