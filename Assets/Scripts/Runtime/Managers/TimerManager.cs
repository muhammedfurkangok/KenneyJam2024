using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimerManager : SingletonMonoBehaviour<TimerManager>
    {
        public float timeScale = 1.0f;
        private float previousTimeScale;
        public bool isPaused;

        public void IncreaseTimeScale()
        {
            if (timeScale < 2f && timeScale > 0f)
            {
                timeScale += 0.5f;
                if (!isPaused)
                {
                    Time.timeScale = timeScale;
                }
            }
        }

        public void DecreaseTimeScale()
        {
            if (timeScale > 0.5f)
            {
                timeScale -= 0.5f;
                if (!isPaused)
                {
                    Time.timeScale = timeScale;
                }
            }
        }

        public void PauseGame()
        {
            if (!isPaused)
            {
                previousTimeScale = Time.timeScale;
                Time.timeScale = 0f;
                isPaused = true;
            }
        }

        public void ResumeGame()
        {
            if (isPaused)
            {
                Time.timeScale = timeScale;
                isPaused = false;
            }
        }
    }
}