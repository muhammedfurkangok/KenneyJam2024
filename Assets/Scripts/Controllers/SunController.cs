using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] Transform sun;
  

    [Tooltip("Day length in seconds.")]
    [SerializeField] float dayLength = 240f;
    /*[SerializeField]*/ float timeScale = 1f;
    private void Start()
    {
        timeScale = 360 / dayLength;
    }

    void Update()
    {
        sun.Rotate(Vector3.right * Time.deltaTime * timeScale);
    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale;
    }
}
