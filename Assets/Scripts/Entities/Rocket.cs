using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1.0f;
    [SerializeField] private float shakeStrength = 0.5f;
    [SerializeField] private int shakeVibrato = 10;
    [SerializeField] private float launchHeight = 50.0f;
    [SerializeField] private float launchDuration = 5.0f;
    [SerializeField] private float backDuration = 5.0f;
    [SerializeField] private Ease launchEase = Ease.InOutQuad;

    private Vector3 StartPosition => transform.position;
    
    

    [Button]
    private void Launch()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato).OnComplete(() =>
        {

            transform.DOLocalMoveY(launchHeight, launchDuration)
                .SetEase(launchEase);
        });
    }

    [Button]
    public void BackToBase()
    {
        transform.DOLocalMoveY(StartPosition.y - 25, backDuration)
            .SetEase(launchEase);
    }
}