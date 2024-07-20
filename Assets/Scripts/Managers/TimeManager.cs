using System;
using Runtime.Extensions;
using UnityEngine;

namespace Managers
{
    public class TimeManager : SingletonMonoBehaviour<TimeManager>
    {
        [Header("Time Settings")]
        [SerializeField] private float cycleDuration = 10f;
        [SerializeField] private float maxTimeScale = 2f;
        [SerializeField] private float minTimeScale = 0.5f;

        [Header("Info - No Touch")]
        [SerializeField] private float currentTime;
        [SerializeField] private float currentTimeScale;
        [SerializeField] private bool isPaused;

        public event Action OnTimeCycleCompleted;

        public float GetCurrentTimeScale() => currentTimeScale;

        private void Start()
        {
            currentTimeScale = 1f;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= cycleDuration)
            {
                currentTime = 0f;
                OnTimeCycleCompleted?.Invoke();
            }
        }

        public void IncreaseTimeScale()
        {
            if (currentTimeScale >= maxTimeScale) return;
            currentTimeScale += 0.5f;

            if (!isPaused) Time.timeScale = currentTimeScale;
        }

        public void DecreaseTimeScale()
        {
            if (currentTimeScale <= minTimeScale) return;
            currentTimeScale -= 0.5f;

            if (!isPaused) Time.timeScale = currentTimeScale;
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = currentTimeScale;
            isPaused = false;
        }
    }
}