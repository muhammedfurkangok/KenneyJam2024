using System.Threading;
using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;

namespace Entities.Vehicles
{
    public class MinerVehicle : VehicleBase
    {
        [Header("Info - No Touch")]
        [SerializeField] private Resource resource;
        [SerializeField] private bool isMining;
        [SerializeField] private int miningAmount;

        private Camera mainCamera;
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

            mainCamera = Camera.main;
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
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 50f, ResourceLayerMask))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    isMining = false;

                    goAndMineCancellationTokenSource?.Cancel();
                    goAndMineCancellationTokenSource = new CancellationTokenSource();

                    resource = hit.collider.GetComponent<Resource>();
                    GoAndMine(goAndMineCancellationTokenSource.Token);
                }
            }
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