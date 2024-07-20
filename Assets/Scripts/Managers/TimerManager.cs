using System;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimerManager : SingletonMonoBehaviour<TimerManager>
    {
        [Header("Info - No Touch")]
        [SerializeField] private float currentTimeScale;
        [SerializeField] private bool isPaused;

        public bool GetIsPaused() => isPaused;

        private void Start()
        {
            currentTimeScale = 1f;
        }

        public void IncreaseTimeScale()
        {
            currentTimeScale = 1.5f;
            if (!isPaused) Time.timeScale = currentTimeScale;
        }

        public void DecreaseTimeScale()
        {
            currentTimeScale = 0.5f;
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