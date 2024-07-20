using UnityEngine;
using DG.Tweening;
using Runtime.Extensions;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [Header("Object UI Settings")]
        [SerializeField] private GameObject objectUIPanel;
        [SerializeField] private Button objectCloseButton; 
        [SerializeField] private float scaleDuration = 0.3f;

        [Header("Timer UI Settings")]
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button increaseSpeedButton;
        [SerializeField] private Button decreaseSpeedButton;

        private bool isUIActive;
        private TimerManager timerManager;

        private void Awake()
        {
            timerManager = TimerManager.Instance;

            if (objectUIPanel != null)
            {
                objectUIPanel.transform.localScale = Vector3.zero;
                objectUIPanel.SetActive(false);
            }

            if (objectCloseButton != null)
            {
                objectCloseButton.onClick.AddListener(HideUI);
            }

            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(PauseGame);
            }

            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(ResumeGame);
            }

            if (increaseSpeedButton != null)
            {
                increaseSpeedButton.onClick.AddListener(IncreaseTimeScale);
            }

            if (decreaseSpeedButton != null)
            {
                decreaseSpeedButton.onClick.AddListener(DecreaseTimeScale);
            }
        }

        public void OpenUI()
        {
            if (objectUIPanel != null)
            {
                objectUIPanel.SetActive(true);
                objectUIPanel.transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
                isUIActive = true;
            }
        }

        public void HideUI()
        {
            if (objectUIPanel != null)
            {
                objectUIPanel.transform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
                {
                    objectUIPanel.SetActive(false);
                });
                isUIActive = false;
            }
        }

        public bool IsUIActive()
        {
            return isUIActive;
        }

        private void PauseGame()
        {
            timerManager.PauseGame();
        }

        private void ResumeGame()
        {
            timerManager.ResumeGame();
        }

        private void IncreaseTimeScale()
        {
            timerManager.IncreaseTimeScale();
            if (timerManager.isPaused)
            {
                timerManager.ResumeGame();
            }
        }

        private void DecreaseTimeScale()
        {
            timerManager.DecreaseTimeScale();
            if (timerManager.isPaused)
            {
                timerManager.ResumeGame();
            }
        }
    }
}
