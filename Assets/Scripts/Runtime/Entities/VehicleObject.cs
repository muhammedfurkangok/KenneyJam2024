using Runtime.Managers;
using UnityEngine;

public class VehicleObject : MonoBehaviour
{
    public float revealRange = 5.0f;
    private FogOfWarManager fogOfWarManager;

    private void Start()
    {
        fogOfWarManager = FindObjectOfType<FogOfWarManager>();
    }

    private void Update()
    {
        fogOfWarManager.RevealArea(transform.position, revealRange);
    }
}