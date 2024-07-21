using UnityEngine;
using DG.Tweening;

namespace Entities
{
    public class Fog : MonoBehaviour
    {
        private void Start()
        {
            StartIdleAnimation();
        }

        private void StartIdleAnimation()
        {
            transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }

        public void RevealFog()
        {
            transform.DOKill();
            
            Sequence revealSequence = DOTween.Sequence();
            revealSequence.Append(transform.DOMoveY(transform.position.y + 4f, 1f).SetEase(Ease.OutQuad));
            revealSequence.Join(transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InQuad));
            revealSequence.OnComplete(() => Destroy(gameObject));
            
            revealSequence.Play();
        }
    }
}