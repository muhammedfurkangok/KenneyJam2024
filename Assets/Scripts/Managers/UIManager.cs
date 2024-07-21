using DG.Tweening;
using Runtime.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [Header("Time UI References")]
        [SerializeField] private Button pauseResumeButton;
        [SerializeField] private Button increaseSpeedButton;
        [SerializeField] private Button decreaseSpeedButton;
        [SerializeField] private TextMeshProUGUI pauseResumeButtonText;

        [Header("Resource UI References")]
        [SerializeField] private TextMeshProUGUI populationText;
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI foodText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI metalText;
        [SerializeField] private TextMeshProUGUI metalPremiumText;
        [SerializeField] private TextMeshProUGUI gemText;

        [Header("Object UI References")]
        [SerializeField] private GameObject objectUIPanel;
        [SerializeField] private Button objectCloseButton;
        [SerializeField] private float scaleDuration = 0.3f;

        [Header("Info - No Touch")]
        private bool isUIActive;

        public bool GetIsUIActive() => isUIActive;

        private void Start()
        {
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

        private void CommonButtonAction()
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

#region ResourceUIMethods

        public void RefreshResourceUI()
        {
            UpdatePopulationText(ResourceManager.Instance.GetResourceAmount(ResourceType.Population));
            UpdateEnergyText(ResourceManager.Instance.GetResourceAmount(ResourceType.Energy));
            UpdateFoodText(ResourceManager.Instance.GetResourceAmount(ResourceType.Food));
            UpdateMoneyText(ResourceManager.Instance.GetResourceAmount(ResourceType.Money));
            UpdateMetalText(ResourceManager.Instance.GetResourceAmount(ResourceType.Metal));
            UpdateMetalPremiumText(ResourceManager.Instance.GetResourceAmount(ResourceType.MetalPremium));
            UpdateGemText(ResourceManager.Instance.GetResourceAmount(ResourceType.Gem));
        }

        public void UpdatePopulationText(int value)
        {
            populationText.text = value.ToString();
        }

        public void UpdateEnergyText(int value)
        {
            energyText.text = value.ToString();
        }

        public void UpdateFoodText(int value)
        {
            foodText.text = value.ToString();
        }

        public void UpdateMoneyText(int value)
        {
            moneyText.text = value.ToString();
        }

        public void UpdateMetalText(int value)
        {
            metalText.text = value.ToString();
        }

        public void UpdateMetalPremiumText(int value)
        {
            metalPremiumText.text = value.ToString();
        }

        public void UpdateGemText(int value)
        {
            gemText.text = value.ToString();
        }

#endregion

#region TimeUIMethods

        private void OnPauseResumeButton()
        {
            CommonButtonAction();

            if (Time.timeScale == 0) ResumeGame();
            else PauseGame();
        }

        private void PauseGame()
        {
            CommonButtonAction();

            TimeManager.Instance.PauseGame();
            pauseResumeButtonText.text = "Paused";
        }

        private void ResumeGame()
        {
            CommonButtonAction();

            TimeManager.Instance.ResumeGame();
            pauseResumeButtonText.text = "Speed: " + Time.timeScale.ToString("0.0");
        }

        private void OnIncreaseTimeScaleButton()
        {
            CommonButtonAction();

            TimeManager.Instance.IncreaseTimeScale();
            pauseResumeButtonText.text = "Speed: " + Time.timeScale.ToString("0.0");
        }

        private void OnDecreaseTimeScaleButton()
        {
            CommonButtonAction();

            TimeManager.Instance.DecreaseTimeScale();
            pauseResumeButtonText.text = "Speed: " + Time.timeScale.ToString("0.0");
        }

#endregion

    }
}
