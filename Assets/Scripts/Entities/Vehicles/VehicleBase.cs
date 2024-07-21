using Data.ScriptableObjects;
using DG.Tweening;
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

        [SerializeField] private GameObject selectionCircle;
        private Tween jumpTween;
        private Tween selectionCircleTween;

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
            SelectionCircleAnimation(true);
            JumpAnimation();
        }

        public void JumpAnimation()
        {
            if (jumpTween != null) jumpTween.Kill();
            var originalPos = transform.position;

            jumpTween = transform.DOJump(transform.position, 0.5f, 1, 0.2f).OnComplete(() => transform.position = originalPos);
        }

        public virtual void Deselect()
        {
            isSelected = false;
            SelectionCircleAnimation(false);
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

        public void SelectionCircleAnimation(bool show)
        {
            if (show)
            {
                if (!selectionCircle.activeSelf) 
                    selectionCircle.SetActive(true);

                if (selectionCircleTween != null) selectionCircleTween.Kill();

                selectionCircleTween = selectionCircle.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.LocalAxisAdd)
                    .SetLoops(-1, LoopType.Incremental);
                selectionCircleTween.Play();
            }
            else
            {
                if (selectionCircle.activeSelf)
                    selectionCircle.SetActive(false);

                if (selectionCircleTween != null)
                    selectionCircleTween.Kill();
            }
        }
        
    }
}
