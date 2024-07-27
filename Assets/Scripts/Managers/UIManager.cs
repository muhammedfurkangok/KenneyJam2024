using Data.ScriptableObjects;
using DG.Tweening;
using Entities.Buildings;
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
        [Header("Resource Data")] [SerializeField] private ResourceData resourceData;

        [Header("Canvas References")]
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private Canvas gameOverCanvas;

        [Header("Game Over References")]
        [SerializeField] private Image blackScreen;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button backToMainMenuButton;

        [Header("Time UI References")]
        [SerializeField] private TextMeshProUGUI gameSpeedText;
        [SerializeField] private Button increaseSpeedButton;
        [SerializeField] private Button decreaseSpeedButton;
        [SerializeField] private Button pauseResumeButton;
        [SerializeField] private Image pauseButtonImage;
        [SerializeField] private Image resumeButtonImage;

        [Header("Resource UI References")]
        [SerializeField] private TextMeshProUGUI populationText;
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI foodText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI metalText;
        [SerializeField] private TextMeshProUGUI metalPremiumText;
        [SerializeField] private TextMeshProUGUI gemText;

        [Header("Building UI Common References")]
        [SerializeField] private Canvas buildingUICanvas;
        [SerializeField] private GameObject buildingDefaultPanel;
        [SerializeField] private GameObject buildingUpgradePanel;
        [SerializeField] private Button upgradePanelButton;
        [SerializeField] private Button backFromUpgradePanelButton;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button closeBuildingUIButton;
        [SerializeField] private TextMeshProUGUI currentTierText;
        [SerializeField] private TextMeshProUGUI currentFoodText;
        [SerializeField] private TextMeshProUGUI currentEnergyText;
        [SerializeField] private TextMeshProUGUI nextFoodText;
        [SerializeField] private TextMeshProUGUI nextEnergyText;
        [SerializeField] private TextMeshProUGUI bPopulationText;
        [SerializeField] private TextMeshProUGUI bMetalText;
        [SerializeField] private TextMeshProUGUI bMetalPremiumText;

        [Header("Building UI HQ References")]
        [SerializeField] private GameObject hqPanel;
        [SerializeField] private TextMeshProUGUI hqPopulationCapacityText;
        [SerializeField] private TextMeshProUGUI hqEnergyRateText;
        [SerializeField] private TextMeshProUGUI hqFoodRateText;

        [Header("Building UI Living Space References")]
        [SerializeField] private GameObject livingSpacePanel;
        [SerializeField] private TextMeshProUGUI livingSpacePopulationText;

        [Header("Building UI Rocket Site References")]
        [SerializeField] private GameObject rocketSitePanel;
        [SerializeField] private Button sellMetalButton;
        [SerializeField] private Button sellMetalPremiumButton;
        [SerializeField] private Button sellGemButton;
        [SerializeField] private Button buyPopulationButton;
        [SerializeField] private Button buyFoodBuFood;
        [SerializeField] private Button buyEnergyButton;

        private GameObject currentBuildingPanel;
        private BuildingType currentBuildingType;
        private BuildingBase currentBuilding;

        private void Start()
        {
            //TimeUI
            gameSpeedText.text = "Speed: " + Time.timeScale.ToString("0.00");
            increaseSpeedButton.onClick.AddListener(OnIncreaseTimeScaleButton);
            decreaseSpeedButton.onClick.AddListener(OnDecreaseTimeScaleButton);
            pauseResumeButton.onClick.AddListener(OnPauseResumeButton);

            //GameOverUI
            backToMainMenuButton.onClick.AddListener(OnBackToMainMenuButton);

            //BuildingUI
            upgradePanelButton.onClick.AddListener(OnUpgradePanelButton);
            backFromUpgradePanelButton.onClick.AddListener(OnBackFromUpgradePanelButton);
            upgradeButton.onClick.AddListener(OnUpgradeButton);
            closeBuildingUIButton.onClick.AddListener(OnCloseBuildingUIButton);

            //BuildingUI - RocketSiteUI
            sellMetalButton.onClick.AddListener(OnSellMetalButton);
            sellMetalPremiumButton.onClick.AddListener(OnSellMetalPremiumButton);
            sellGemButton.onClick.AddListener(OnSellGemButton);
            buyPopulationButton.onClick.AddListener(OnBuyPopulationButton);
            buyFoodBuFood.onClick.AddListener(OnBuyFoodButton);
            buyEnergyButton.onClick.AddListener(OnBuyEnergyButton);
        }

        private void CommonButtonAction()
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

#region EditorMethods
#if UNITY_EDITOR

        [Button]
        public void ArrangeResourceIcons(float leftStart, float height, float interval)
        {
            populationText.rectTransform.parent.localPosition = new Vector3(leftStart, height, 0);
            energyText.rectTransform.parent.localPosition = new Vector3(leftStart + interval, height, 0);
            foodText.rectTransform.parent.localPosition = new Vector3(leftStart + interval * 2, height, 0);
            moneyText.rectTransform.parent.localPosition = new Vector3(leftStart + interval * 3, height, 0);
            metalText.rectTransform.parent.localPosition = new Vector3(leftStart + interval * 4, height, 0);
            metalPremiumText.rectTransform.parent.localPosition = new Vector3(leftStart + interval * 5, height, 0);
            gemText.rectTransform.parent.localPosition = new Vector3(leftStart + interval * 6, height, 0);
        }

#endif
#endregion

#region GameOverUI

        public async void ShowGameOverUI(string resourceName)
        {
            GameManager.Instance.ChangeGameState(GameState.UI);

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
            populationText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Population).ToString("000");
            energyText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Energy).ToString("000");
            foodText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Food).ToString("000");
            moneyText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Money).ToString("000");
            metalText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Metal).ToString("000");
            metalPremiumText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.MetalPremium).ToString("000");
            gemText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Gem).ToString("000");
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
            gameSpeedText.text = "Paused";

            resumeButtonImage.gameObject.SetActive(true);
            pauseButtonImage.gameObject.SetActive(false);
        }

        private void ResumeGame()
        {
            CommonButtonAction();

            TimeManager.Instance.ResumeGame();
            gameSpeedText.text = "Speed: " + Time.timeScale.ToString("0.00");

            resumeButtonImage.gameObject.SetActive(false);
            pauseButtonImage.gameObject.SetActive(true);
        }

        private void OnIncreaseTimeScaleButton()
        {
            CommonButtonAction();

            TimeManager.Instance.IncreaseTimeScale();
            gameSpeedText.text = "Speed: " + Time.timeScale.ToString("0.00");
        }

        private void OnDecreaseTimeScaleButton()
        {
            CommonButtonAction();

            TimeManager.Instance.DecreaseTimeScale();
            gameSpeedText.text = "Speed: " + Time.timeScale.ToString("0.00");
        }

#endregion

#region BuildingUIMethods

        public void OpenBuildingUI(BuildingBase building)
        {
            GameManager.Instance.ChangeGameState(GameState.UI);

            currentBuilding = building;
            currentBuildingType = currentBuilding.GetBuildingType();

            buildingUICanvas.gameObject.SetActive(true);
            BuildingTypeToPanel(currentBuildingType).SetActive(true);
        }

        private GameObject BuildingTypeToPanel(BuildingType buildingType)
        {
            if (buildingType == BuildingType.HQ) return hqPanel;
            else if (buildingType == BuildingType.LivingSpace) return livingSpacePanel;
            else if (buildingType == BuildingType.RocketSite) return rocketSitePanel;
            else return buildingDefaultPanel;
        }

        private void OnUpgradePanelButton()
        {
            CommonButtonAction();

            buildingDefaultPanel.SetActive(false);
            buildingUpgradePanel.SetActive(true);
            currentBuildingPanel.SetActive(false);
        }

        private void OnBackFromUpgradePanelButton()
        {
            CommonButtonAction();

            buildingDefaultPanel.SetActive(true);
            buildingUpgradePanel.SetActive(false);
            currentBuildingPanel.SetActive(true);
        }

        private void OnUpgradeButton()
        {
            CommonButtonAction();

            currentBuilding.Upgrade();
        }

        private void OnCloseBuildingUIButton()
        {
            GameManager.Instance.ChangeGameState(GameState.Free);

            CommonButtonAction();

            ResetBuildingUI();
            buildingUICanvas.gameObject.SetActive(false);
        }

        private void ResetBuildingUI()
        {
            buildingDefaultPanel.SetActive(true);

            buildingUpgradePanel.SetActive(false);
            hqPanel.SetActive(false);
            livingSpacePanel.SetActive(false);
            rocketSitePanel.SetActive(false);
        }

        private void OnSellMetalButton()
        {
            CommonButtonAction();

            ResourceManager.Instance.DecreaseResource(ResourceType.Metal, 1);
            ResourceManager.Instance.IncreaseResource(ResourceType.Money, resourceData.GetResourceMoneyValue(ResourceType.Metal));
        }

        private void OnSellMetalPremiumButton()
        {
            CommonButtonAction();

            ResourceManager.Instance.DecreaseResource(ResourceType.MetalPremium, 1);
            ResourceManager.Instance.IncreaseResource(ResourceType.Money, resourceData.GetResourceMoneyValue(ResourceType.MetalPremium));
        }

        private void OnSellGemButton()
        {
            CommonButtonAction();

            ResourceManager.Instance.DecreaseResource(ResourceType.Gem, 1);
            ResourceManager.Instance.IncreaseResource(ResourceType.Money, resourceData.GetResourceMoneyValue(ResourceType.Gem));
        }

        private void OnBuyPopulationButton()
        {
            CommonButtonAction();

            if (ResourceManager.Instance.GetResourceAmount(ResourceType.Population) == ResourceManager.Instance.GetResourceAmount(ResourceType.PopulationCapacity))
                return;

            ResourceManager.Instance.DecreaseResource(ResourceType.Money, resourceData.GetResourceMoneyValue(ResourceType.Population));
            ResourceManager.Instance.IncreaseResource(ResourceType.Population, 1);
        }

        private void OnBuyFoodButton()
        {
            CommonButtonAction();

            ResourceManager.Instance.DecreaseResource(ResourceType.Money, resourceData.GetResourceMoneyValue(ResourceType.Food));
            ResourceManager.Instance.IncreaseResource(ResourceType.Food, 1);
        }

        private void OnBuyEnergyButton()
        {
            CommonButtonAction();

            ResourceManager.Instance.DecreaseResource(ResourceType.Money, resourceData.GetResourceMoneyValue(ResourceType.Energy));
            ResourceManager.Instance.IncreaseResource(ResourceType.Energy, 1);
        }

#endregion

    }
}
