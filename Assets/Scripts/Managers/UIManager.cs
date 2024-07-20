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

        protected override void Awake()
        {
            base.Awake();

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
            if (Time.timeScale == 0) ResumeGame();
            else PauseGame();
        }

        private void PauseGame()
        {
            TimerManager.Instance.PauseGame();
            pauseResumeButtonText.text = "Resume";
        }

        private void ResumeGame()
        {
            TimerManager.Instance.ResumeGame();
            pauseResumeButtonText.text = "Pause";
        }

        private void IncreaseTimeScale()
        {
            TimerManager.Instance.IncreaseTimeScale();
            if (TimerManager.Instance.GetIsPaused()) ResumeGame();
        }

        private void DecreaseTimeScale()
        {
            TimerManager.Instance.DecreaseTimeScale();
            if (TimerManager.Instance.GetIsPaused()) ResumeGame();
        }
    }
}
