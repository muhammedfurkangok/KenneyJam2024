using Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Vehicles
{
    public abstract class VehicleBase : Upgradeable
    {
        [Header("Vehicle Base - References")]
        [SerializeField] private VehicleData vehicleData;
        [SerializeField] private NavMeshAgent navMeshAgent;

        [Header("Vehicle Base - Parameters")]
        [SerializeField] private VehicleType vehicleType;

        [Header("Vehicle Base - Info - No Touch")]
        [SerializeField] private bool isSelected;

        protected virtual void Start()
        {
            SetSpeeds();
        }

        private void SetSpeeds()
        {
            var speedData = vehicleData.GetSpeeds(vehicleType, tier);
            navMeshAgent.speed = speedData.speed;
            navMeshAgent.angularSpeed = speedData.angularSpeed;
            navMeshAgent.acceleration = speedData.acceleration;
        }

        public virtual void Select()
        {
            isSelected = true;
        }

        public virtual void Deselect()
        {
            isSelected = false;
            Stop();
        }

        public virtual void Move(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }

        public virtual void Stop()
        {
            navMeshAgent.SetDestination(transform.position + 1f * transform.forward);
        }

        public override void Upgrade()
        {
            base.Upgrade();

            SetSpeeds();
        }
    }
}
