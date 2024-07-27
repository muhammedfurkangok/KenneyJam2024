using DG.Tweening;
using UnityEngine;

namespace Data.ScriptableObjects
{
    //[CreateAssetMenu(fileName = "EntityParameters", menuName = "EntityParameters", order = 0)]
    public class EntityParameters : ScriptableObject
    {
        [Header("Highlight Parameters")]
        public Color highlightColor;

        [Header("Rocket Parameters")]
        public float rocketShakeDuration;
        public float rocketShakeStrength;
        public int rocketShakeVibrato;
        public float rocketLaunchHeight;
        public float rocketLaunchDuration;
        public float rocketBackDuration;
        public Ease rocketLaunchEase;
    }
}