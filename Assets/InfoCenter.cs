using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCenter : MonoBehaviour
{
    private float cycleDuration;

    private void Start()
    {
        cycleDuration = TimeManager.Instance.GetCycleDuration();
    }

    public void GetResourseDepletionTime(ResourceType type)
    {

    }

    void CalculateResourceDepletionTime()
    {
    }
}
