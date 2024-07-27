using System.Threading;
using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;

namespace Entities.Vehicles
{
    public class MinerVehicle : VehicleBase
    {
        [Header("Miner Vehicle - Info - No Touch")]
        [SerializeField] private Resource resource;
        [SerializeField] private bool isMining;
        [SerializeField] private int miningAmount;

        private CancellationTokenSource goAndMineCancellationTokenSource;

        private const int ResourceLayerMask = 1<<8;

        public override void Upgrade()
        {
            base.Upgrade();

            miningAmount = vehicleData.GetMinerMiningAmount(tier);
        }

        protected override void Start()
        {
            base.Start();

            miningAmount = vehicleData.GetMinerMiningAmount(tier);
            TimeManager.Instance.OnTimeCycleCompleted += MineResource;
        }

        private void OnDisable()
        {
            TimeManager.Instance.OnTimeCycleCompleted -= MineResource;
        }

        private void Update()
        {
            if (isSelected && InputManager.Instance.GetInputMouseLeftClick()) CheckForMine();
        }

        private void CheckForMine()
        {
            if (!RaycastManager.Instance.RaycastFromScreenPoint(Input.mousePosition, 50f, ResourceLayerMask, out var hit)) return;
            if (hit.collider.gameObject.layer != 8) return;

            isMining = false;

            goAndMineCancellationTokenSource?.Cancel();
            goAndMineCancellationTokenSource = new CancellationTokenSource();

            resource = hit.collider.GetComponent<Resource>();
            GoAndMine(goAndMineCancellationTokenSource.Token);
        }

        public override void StopInstantly()
        {
            goAndMineCancellationTokenSource?.Cancel();
            base.StopInstantly();
        }

        public override void StopWithDistance()
        {
            goAndMineCancellationTokenSource?.Cancel();
            base.StopWithDistance();
        }

        private async void GoAndMine(CancellationToken cancellationToken)
        {
            Move(resource.transform.position);
            await UniTask.WaitWhile(() => navMeshAgent.pathPending, cancellationToken: cancellationToken).SuppressCancellationThrow();

            if (cancellationToken.IsCancellationRequested)
            {
                StopWithDistance();
                return;
            }

            await UniTask.WaitWhile(() => navMeshAgent.remainingDistance < 0.1f, cancellationToken: cancellationToken).SuppressCancellationThrow();

            if (cancellationToken.IsCancellationRequested)
            {
                StopInstantly();
                return;
            }

            isMining = true;
        }

        private void MineResource()
        {
            if (!isMining) return;

            resource.MineResource(miningAmount);
            ResourceManager.Instance.IncreaseResource(resource.GetResourceType(), miningAmount);
        }
    }
}