using Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Vehicles
{
    public abstract class VehicleBase : Upgradeable
    {
        [Header("Vehicle Base - References")]
        [SerializeField] protected VehicleData vehicleData;
        [SerializeField] protected NavMeshAgent navMeshAgent;

        [Header("Vehicle Base - Parameters")]
        [SerializeField] private VehicleType vehicleType;

        [Header("Vehicle Base - Info - No Touch")]
        [SerializeField] protected bool isSelected;

        public VehicleType GetVehicleType() => vehicleType;
        public bool IsVehicleMoving() => navMeshAgent.velocity.magnitude > 0.1f;

        public override void Upgrade()
        {
            base.Upgrade();

            SetSpeeds();
        }

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

            if (IsVehicleMoving()) StopWithDistance();
            else StopInstantly();
        }

        public virtual void Move(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }

        public virtual void StopWithDistance()
        {
            navMeshAgent.SetDestination(transform.position + 1f * transform.forward);
        }

        public virtual void StopInstantly()
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }
}
