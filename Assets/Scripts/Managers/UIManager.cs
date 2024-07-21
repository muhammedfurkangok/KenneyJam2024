using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [Header("Canvas References")]
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private Canvas gameOverCanvas;

        [Header("Game Over References")]
        [SerializeField] private Image blackScreen;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button backToMainMenuButton;

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

        [Header("Info - No Touch")]
        private bool isUIActive;

        public bool GetIsUIActive() => isUIActive;

        private void Start()
        {
            //TimeUI
            pauseResumeButton.onClick.AddListener(OnPauseResumeButton);
            increaseSpeedButton.onClick.AddListener(OnIncreaseTimeScaleButton);
            decreaseSpeedButton.onClick.AddListener(OnDecreaseTimeScaleButton);

            //LevelFailUI
            backToMainMenuButton.onClick.AddListener(OnBackToMainMenuButton);
        }

        private void CommonButtonAction()
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }


#region LevelFail

        [Button]
        public async void ShowGameFailUI(string resourceName)
        {
            mainCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(true);

            gameOverText.text = "You ran out of " + resourceName + "!";
            await blackScreen.DOColor(Color.black, 1f).SetUpdate(true).AsyncWaitForCompletion();
            await gameOverText.DOFade(1, 1f).SetUpdate(true).AsyncWaitForCompletion();
            await backToMainMenuButton.gameObject.transform.DOScale(1, 1f).SetUpdate(true).AsyncWaitForCompletion();
        }

        private void OnBackToMainMenuButton()
        {
            CommonButtonAction();

            SceneManager.LoadScene(0);
        }

        #endregion

#region ResourceUIMethods

        public void RefreshResourceUI()
        {
            populationText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Population).ToString();
            energyText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Energy).ToString();
            foodText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Food).ToString();
            moneyText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Money).ToString();
            metalText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Metal).ToString();
            metalPremiumText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.MetalPremium).ToString();
            gemText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Gem).ToString();
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
