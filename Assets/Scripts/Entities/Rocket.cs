using Data.ScriptableObjects;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Entities
{
    public class Rocket : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EntityParameters entityParameters;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        [Button]
        private void Launch()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOShakePosition(entityParameters.rocketShakeDuration, entityParameters.rocketShakeStrength, entityParameters.rocketShakeVibrato));
            sequence.Append(transform.DOLocalMoveY(entityParameters.rocketLaunchHeight, entityParameters.rocketLaunchDuration).SetEase(entityParameters.rocketLaunchEase));
            sequence.Play();
        }

        [Button]
        public void BackToBase()
        {
            transform.DOLocalMoveY(startPosition.y, entityParameters.rocketBackDuration).SetEase(entityParameters.rocketLaunchEase);
        }
    }
}