using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Entities
{
    public class Rocket : MonoBehaviour
    {
        [Header("Parameters")] //todo: move to entity parameters
        [SerializeField] private float shakeDuration = 1.0f;
        [SerializeField] private float shakeStrength = 0.5f;
        [SerializeField] private int shakeVibrato = 10;
        [SerializeField] private float launchHeight = 50.0f;
        [SerializeField] private float launchDuration = 5.0f;
        [SerializeField] private float backDuration = 5.0f;
        [SerializeField] private Ease launchEase = Ease.InOutQuad;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        [Button]
        private void Launch()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato));
            sequence.Append(transform.DOLocalMoveY(launchHeight, launchDuration).SetEase(launchEase));
            sequence.Play();
        }

        [Button]
        public void BackToBase()
        {
            transform.DOLocalMoveY(startPosition.y, backDuration).SetEase(launchEase);
        }
    }
}