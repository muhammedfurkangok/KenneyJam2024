using System;
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
        [Header("Data References")]
        [SerializeField] private BuildingData buildingData;
        [SerializeField] private ResourceData resourceData;

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

        [Header("Resource UI Parameters")]
        [SerializeField] private Color criticalResourceColor;
        [SerializeField] private Color fullResourceColor;

        [Header("Building UI Common References")]
        [SerializeField] private Canvas buildingUICanvas;
        [SerializeField] private GameObject buildingDefaultPanel;
        [SerializeField] private GameObject buildingUpgradePanel;
        [SerializeField] private Button upgradePanelButton;
        [SerializeField] private Button backFromUpgradePanelButton;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button closeBuildingUIButton;
        [SerializeField] private TextMeshProUGUI currentTierValueText;
        [SerializeField] private TextMeshProUGUI currentEnergyText;
        [SerializeField] private TextMeshProUGUI currentFoodText;
        [SerializeField] private TextMeshProUGUI upgradePopulationText;
        [SerializeField] private TextMeshProUGUI upgradeMetalText;
        [SerializeField] private TextMeshProUGUI upgradeMetalPremiumText;
        [SerializeField] private TextMeshProUGUI nextEnergyText;
        [SerializeField] private TextMeshProUGUI nextFoodText;

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
        [SerializeField] private Button buyEnergyButton;
        [SerializeField] private Button buyFoodButton;
        [SerializeField] private TextMeshProUGUI metalMoneyAmountText;
        [SerializeField] private TextMeshProUGUI metalPremiumMoneyAmountText;
        [SerializeField] private TextMeshProUGUI gemMoneyAmountText;
        [SerializeField] private TextMeshProUGUI populationMoneyAmountText;
        [SerializeField] private TextMeshProUGUI energyMoneyAmountText;
        [SerializeField] private TextMeshProUGUI foodMoneyAmountText;
        [SerializeField] private TextMeshProUGUI metalOnCartText;
        [SerializeField] private TextMeshProUGUI metalPremiumOnCartText;
        [SerializeField] private TextMeshProUGUI gemOnCartText;
        [SerializeField] private TextMeshProUGUI populationOnCartText;
        [SerializeField] private TextMeshProUGUI energyOnCartText;
        [SerializeField] private TextMeshProUGUI foodOnCartText;
        [SerializeField] private TextMeshProUGUI moneyBalanceText;
        [SerializeField] private TextMeshProUGUI goCapacityText;
        [SerializeField] private TextMeshProUGUI returnCapacityText;

        [Header("General Info - No Touch")]
        [SerializeField] private BuildingBase currentBuilding;
        [SerializeField] private BuildingType currentBuildingType;
        [SerializeField] private GameObject currentBuildingPanel;
        [SerializeField] private Color defaultResourceAmountColor;

        private void Start()
        {
            //ResourceUI
            defaultResourceAmountColor = populationText.color;

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
            buyEnergyButton.onClick.AddListener(OnBuyEnergyButton);
            buyFoodButton.onClick.AddListener(OnBuyFoodButton);

            //BuildingUI - RocketSiteUI
            metalMoneyAmountText.text = resourceData.GetResourceMoneyValue(ResourceType.Metal).ToString("000");
            metalPremiumMoneyAmountText.text = resourceData.GetResourceMoneyValue(ResourceType.MetalPremium).ToString("000");
            gemMoneyAmountText.text = resourceData.GetResourceMoneyValue(ResourceType.Gem).ToString("000");
            populationMoneyAmountText.text = resourceData.GetResourceMoneyValue(ResourceType.Population).ToString("000");
            foodMoneyAmountText.text = resourceData.GetResourceMoneyValue(ResourceType.Food).ToString("000");
            energyMoneyAmountText.text = resourceData.GetResourceMoneyValue(ResourceType.Energy).ToString("000");
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

            var currentPopulation = ResourceManager.Instance.GetResourceAmount(ResourceType.Population);
            var currentPopulationCapacity = ResourceManager.Instance.GetResourceAmount(ResourceType.PopulationCapacity);

            populationText.color = currentPopulation == currentPopulationCapacity ? fullResourceColor : defaultResourceAmountColor;
            energyText.color = ResourceManager.Instance.IsResourceCritical(ResourceType.Energy) ? criticalResourceColor : defaultResourceAmountColor;
            foodText.color = ResourceManager.Instance.IsResourceCritical(ResourceType.Food) ? criticalResourceColor : defaultResourceAmountColor;
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

#endregion TimeUIMethods

#region BuildingUIMethods

#region BuildingUICommonMethods

        private GameObject BuildingTypeToPanel(BuildingType buildingType)
        {
            if (buildingType == BuildingType.HQ) return hqPanel;
            else if (buildingType == BuildingType.LivingSpace) return livingSpacePanel;
            else if (buildingType == BuildingType.RocketSite) return rocketSitePanel;

            //Add new building types here

            throw new Exception("Building type panel not found: " + buildingType);
        }

        private void RefreshBuildingTypeUI(BuildingType buildingType)
        {
            if (buildingType == BuildingType.HQ) RefreshHQPanel();
            else if (buildingType == BuildingType.LivingSpace) RefreshLivingSpacePanel();

            else if (buildingType == BuildingType.RocketSite) RefreshRocketSitePanel();

            //Add new building types here
        }

        private void RefreshBuildingDefaultUI(BuildingBase building)
        {
            currentTierValueText.text = building.GetTier().ToString("000");
            currentEnergyText.text = buildingData.GetMaintainCost(building.GetBuildingType(), building.GetTier(), ResourceType.Energy).ToString("000");
            currentFoodText.text = buildingData.GetMaintainCost(building.GetBuildingType(), building.GetTier(), ResourceType.Food).ToString("000");
        }

        private void RefreshBuildingUpgradeUI(BuildingBase building)
        {
            var nextTier = building.GetTier() + 1;

            upgradePopulationText.text = buildingData.GetBuildCost(building.GetBuildingType(), nextTier, ResourceType.Population).ToString("000");
            upgradeMetalText.text = buildingData.GetBuildCost(building.GetBuildingType(), nextTier, ResourceType.Metal).ToString("000");
            upgradeMetalPremiumText.text = buildingData.GetBuildCost(building.GetBuildingType(), nextTier, ResourceType.MetalPremium).ToString("000");

            nextEnergyText.text = buildingData.GetMaintainCost(building.GetBuildingType(), nextTier, ResourceType.Energy).ToString("000");
            nextFoodText.text = buildingData.GetMaintainCost(building.GetBuildingType(), nextTier, ResourceType.Food).ToString("000");
        }

        private void ResetBuildingUI()
        {
            buildingDefaultPanel.SetActive(true);

            buildingUpgradePanel.SetActive(false);
            hqPanel.SetActive(false);
            livingSpacePanel.SetActive(false);

            rocketSitePanel.SetActive(false);
            TradeManager.Instance.ClearTradeData();

            //Add new building panels here
        }

        public void OpenBuildingUI(BuildingBase building)
        {
            GameManager.Instance.ChangeGameState(GameState.UI);

            currentBuilding = building;
            currentBuildingType = currentBuilding.GetBuildingType();
            currentBuildingPanel = BuildingTypeToPanel(currentBuildingType);

            buildingUICanvas.gameObject.SetActive(true);
            currentBuildingPanel.SetActive(true);

            RefreshBuildingDefaultUI(building);
            RefreshBuildingTypeUI(currentBuildingType);
        }

        private void OnCloseBuildingUIButton()
        {
            GameManager.Instance.ChangeGameState(GameState.Free);

            CommonButtonAction();

            ResetBuildingUI();
            buildingUICanvas.gameObject.SetActive(false);
        }

        private void OnUpgradePanelButton()
        {
            CommonButtonAction();

            buildingDefaultPanel.SetActive(false);
            buildingUpgradePanel.SetActive(true);

            currentBuildingPanel.SetActive(false);
            RefreshBuildingUpgradeUI(currentBuilding);
        }

        private void OnBackFromUpgradePanelButton()
        {
            CommonButtonAction();

            buildingDefaultPanel.SetActive(true);
            buildingUpgradePanel.SetActive(false);

            currentBuildingPanel.SetActive(true);
            RefreshBuildingDefaultUI(currentBuilding);
            RefreshBuildingTypeUI(currentBuildingType);
        }

        private void OnUpgradeButton()
        {
            CommonButtonAction();

            currentBuilding.Upgrade();
            RefreshBuildingDefaultUI(currentBuilding);
        }

#endregion BuildingUICommonMethods

#region BuildingUIHQMethods

        private void RefreshHQPanel()
        {
            hqPopulationCapacityText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.PopulationCapacity).ToString("000");
            hqEnergyRateText.text = ResourceManager.Instance.GetResourceConsumeAmount(ResourceType.Energy).ToString("000");
            hqFoodRateText.text = ResourceManager.Instance.GetResourceConsumeAmount(ResourceType.Food).ToString("000");
        }

#endregion BuildingUIHQMethods

#region BuildingUILivingSpaceMethods

        private void RefreshLivingSpacePanel()
        {
            livingSpacePopulationText.text = ResourceManager.Instance.GetResourceAmount(ResourceType.Population).ToString("000");
        }

#endregion

#region BuildingUIRocketSiteMethods

        private void RefreshRocketSitePanel()
        {
            TradeManager.Instance.currentRocketSite = currentBuilding as RocketSite;

            metalOnCartText.text = TradeManager.Instance.GetResourceAmountOnCart(ResourceType.Metal, false).ToString("000");
            metalPremiumOnCartText.text = TradeManager.Instance.GetResourceAmountOnCart(ResourceType.MetalPremium, false).ToString("000");
            gemOnCartText.text = TradeManager.Instance.GetResourceAmountOnCart(ResourceType.Gem, false).ToString("000");
            populationOnCartText.text = TradeManager.Instance.GetResourceAmountOnCart(ResourceType.Population, true).ToString("000");
            energyOnCartText.text = TradeManager.Instance.GetResourceAmountOnCart(ResourceType.Energy, true).ToString("000");
            foodOnCartText.text = TradeManager.Instance.GetResourceAmountOnCart(ResourceType.Food, true).ToString("000");

            moneyBalanceText.text = "Money Balance: " + TradeManager.Instance.GetMoneyInCart().ToString("000");
            moneyBalanceText.color = TradeManager.Instance.GetMoneyInCart() < 0 ? criticalResourceColor : fullResourceColor;

            goCapacityText.text = "GO Capacity: " + TradeManager.Instance.GetSellCartSize().ToString("000");
            returnCapacityText.text = "Return Capacity: " + TradeManager.Instance.GetBuyCartSize().ToString("000");
        }

        private void OnSellMetalButton()
        {
            CommonButtonAction();

            TradeManager.Instance.TryAddToCart(ResourceType.Metal, 1, false);
            RefreshRocketSitePanel();
        }

        private void OnSellMetalPremiumButton()
        {
            CommonButtonAction();

            TradeManager.Instance.TryAddToCart(ResourceType.MetalPremium, 1, false);
            RefreshRocketSitePanel();
        }

        private void OnSellGemButton()
        {
            CommonButtonAction();

            TradeManager.Instance.TryAddToCart(ResourceType.Gem, 1, false);
            RefreshRocketSitePanel();
        }

        private void OnBuyPopulationButton()
        {
            CommonButtonAction();

            TradeManager.Instance.TryAddToCart(ResourceType.Population, 1, true);
            RefreshRocketSitePanel();
        }

        private void OnBuyFoodButton()
        {
            CommonButtonAction();

            TradeManager.Instance.TryAddToCart(ResourceType.Food, 1, true);
            RefreshRocketSitePanel();
        }

        private void OnBuyEnergyButton()
        {
            CommonButtonAction();

            TradeManager.Instance.TryAddToCart(ResourceType.Energy, 1, true);
            RefreshRocketSitePanel();
        }

#endregion BuildingUIRocketSiteMethods

#endregion BuildingUIMethods

    }
}
