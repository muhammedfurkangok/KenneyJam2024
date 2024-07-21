using UnityEngine;
using UnityEngine.AI;

namespace Entities.Vehicles
{
    public abstract class VehicleBase : MonoBehaviour
    {
        [Header("Vehicle Base - References")]
        [SerializeField] private NavMeshAgent navMeshAgent;

        private bool isSelected;

        public void Select()
        {
            isSelected = true;
        }

        public void Deselect()
        {
            isSelected = false;
        }

        public void Move(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }
    }
}
