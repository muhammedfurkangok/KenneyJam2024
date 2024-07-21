using System;
using UnityEngine;

namespace Data.ScriptableObjects
{
    //[CreateAssetMenu(fileName = "VehicleData", menuName = "VehicleData", order = 0)]
    public class VehicleData : ScriptableObject
    {
        [Header("Vehicle Speeds")]
        [SerializeField] private VehicleSpeedData[] vehicleSpeedDatas;
        [SerializeField] private int[] minerMiningAmounts;

        public SpeedInfo GetSpeeds(VehicleType vehicleType, int tier)
        {
            foreach (var vehicleSpeedData in vehicleSpeedDatas)
            {
                if (vehicleSpeedData.type == vehicleType && vehicleSpeedData.tier == tier)
                {
                    return vehicleSpeedData.speedInfo;
                }
            }

            throw new Exception("Vehicle speed not found for: " + vehicleType + " " + tier);
        }

        public int GetMinerMiningAmount(int tier)
        {
            return minerMiningAmounts[tier];
        }
    }
}