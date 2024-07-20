using DG.Tweening;
using Runtime.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [Header("Object UI Settings")]
        [SerializeField] private GameObject objectUIPanel;
        [SerializeField] private Button objectCloseButton; 
        [SerializeField] private float scaleDuration = 0.3f;

        [Header("Time UI Settings")]
        [SerializeField] private Button pauseResumeButton;
        [SerializeField] private Button increaseSpeedButton;
        [SerializeField] private Button decreaseSpeedButton;
        [SerializeField] private TextMeshProUGUI pauseResumeButtonText;
        [SerializeField] private TextMeshProUGUI currentTimeScaleText;

        private bool isUIActive;

        protected override void Awake()
        {
            base.Awake();

            objectUIPanel.SetActive(false);

            //Time UI
            pauseResumeButton.onClick.AddListener(OnPauseResumeButton);
            increaseSpeedButton.onClick.AddListener(OnIncreaseTimeScaleButton);
            decreaseSpeedButton.onClick.AddListener(OnDecreaseTimeScaleButton);
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

        private void OnPauseResumeButton()
        {
            if (Time.timeScale == 0) ResumeGame();
            else PauseGame();
        }

        private void PauseGame()
        {
            TimeManager.Instance.PauseGame();
            pauseResumeButtonText.text = "Resume";
        }

        private void ResumeGame()
        {
            TimeManager.Instance.ResumeGame();
            pauseResumeButtonText.text = "Pause";
        }

        private void OnIncreaseTimeScaleButton()
        {
            TimeManager.Instance.IncreaseTimeScale();
            currentTimeScaleText.text = Time.timeScale.ToString("0.0");
        }

        private void OnDecreaseTimeScaleButton()
        {
            TimeManager.Instance.DecreaseTimeScale();
            currentTimeScaleText.text = Time.timeScale.ToString("0.0");
        }
    }
}
