using System;
using Managers;
using UnityEngine;

namespace Entities
{
    public class FogExplorerObject : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fog"))
            {
                other.GetComponent<FogObject>().RevealFog();
                
            }
        }
    }
}