using UnityEngine;
using DG.Tweening;
using Runtime.Extensions;
using TMPro;
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
        [SerializeField] private Button pauseResumeButton;
        [SerializeField] private TextMeshProUGUI pauseResumeButtonText;
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

            if (pauseResumeButton != null)
            {
                pauseResumeButton.onClick.AddListener(TogglePauseResume);
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

        private void TogglePauseResume()
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void PauseGame()
        {
            timerManager.PauseGame();
            if (pauseResumeButtonText != null)
            {
                //ikon olarak değiştirilsin
                pauseResumeButtonText.text = "Resume";
            }
        }

        private void ResumeGame()
        {
            timerManager.ResumeGame();
            if (pauseResumeButtonText != null)
            {
                //ikon olarak değiştirilsin
                pauseResumeButtonText.text = "Pause";
            }
        }

        private void IncreaseTimeScale()
        {
            timerManager.IncreaseTimeScale();
            if (timerManager.isPaused)
            {
                ResumeGame();
            }
        }

        private void DecreaseTimeScale()
        {
            timerManager.DecreaseTimeScale();
            if (timerManager.isPaused)
            {
                ResumeGame();
            }
        }
    }
}
