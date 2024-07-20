using UnityEngine;

namespace Runtime.Data
{
    //[CreateAssetMenu(fileName = "EntityParameters", menuName = "EntityParameters", order = 0)]
    public class EntityParameters : ScriptableObject
    {
        [Header("Highlight Parameters")]
        public Color highlightColor;
        public float jumpHeight;
        public float jumpDuration;
    }
}