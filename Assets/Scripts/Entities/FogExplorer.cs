using UnityEngine;

namespace Entities
{
    public class FogExplorer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fog")) other.GetComponent<Fog>().RevealFog();
        }
    }
}