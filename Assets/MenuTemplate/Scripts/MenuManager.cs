using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuTemplate.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        //PlayerPref keys
        private const string MasterVolume = "MasterVolume";
        private const string MusicVolume = "MusicVolume";
        private const string SFXVolume = "SFXVolume";
        private const string Sensitivity = "Sensitivity";

        [Header("Main Menu Or Pause Menu")] [SerializeField] private bool isMainMenu;

        [Header("References - Audio")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonClickSound;
        [SerializeField] private AudioClip sliderUpSound;
        [SerializeField] private AudioClip sliderDownSound;
        [SerializeField] private AudioClip toggleOnSound;
        [SerializeField] private AudioClip toggleOffSound;

        [Header("References - Panels")]
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject howToPlayPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private GameObject videoPanel;
        [SerializeField] private GameObject audioPanel;
        [SerializeField] private GameObject controlsPanel;

        [Header("References - Main Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button howToPlayButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button quitButton;

        [Header("References - How To Play Buttons")]
        [SerializeField] private Button backFromHowToPlayButton;

        [Header("References - Settings Buttons")]
        [SerializeField] private Button videoButton;
        [SerializeField] private Button audioButton;
        [SerializeField] private Button controlsButton;
        [SerializeField] private Button backFromSettingsButton;

        [Header("References - Video Settings: Sliders, Texts and Buttons")]
        [SerializeField] private Button backFromVideoButton;

        [Header("References - Audio Settings: Sliders, Texts and Buttons")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private TextMeshProUGUI masterVolumeAmountText;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private TextMeshProUGUI musicVolumeAmountText;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private TextMeshProUGUI sfxVolumeAmountText;
        [SerializeField] private Button backFromAudioButton;

        [Header("References - Controls Settings: Sliders, Texts and Buttons")]
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private TextMeshProUGUI sensitivityAmountText;
        [SerializeField] private Button backFromControlsButton;

        [Header("References - Credits Buttons")]
        [SerializeField] private Button backFromCreditsButton;

        private void Start()
        {
            //Set initial slider values
            masterVolumeSlider.value = PlayerPrefs.GetInt(MasterVolume, 100);
            masterVolumeAmountText.text = ((int)masterVolumeSlider.value).ToString();
            musicVolumeSlider.value = PlayerPrefs.GetInt(MusicVolume, 100);
            musicVolumeAmountText.text = ((int)musicVolumeSlider.value).ToString();
            sfxVolumeSlider.value = PlayerPrefs.GetInt(SFXVolume, 100);
            sfxVolumeAmountText.text = ((int)sfxVolumeSlider.value).ToString();
            sensitivitySlider.value = PlayerPrefs.GetInt(Sensitivity, 100);
            sensitivityAmountText.text = ((int)sensitivitySlider.value).ToString();

            //Set initial audio mixer values
            audioMixer.SetFloat(MasterVolume, GetAudioMixerValue(masterVolumeSlider.value));
            audioMixer.SetFloat(MusicVolume, GetAudioMixerValue(musicVolumeSlider.value));
            audioMixer.SetFloat(SFXVolume, GetAudioMixerValue(sfxVolumeSlider.value));

            //Set start/continue button active
            startButton.gameObject.SetActive(isMainMenu);
            continueButton.gameObject.SetActive(!isMainMenu);

            //Main buttons
            startButton.onClick.AddListener(OnStartButton);
            continueButton.onClick.AddListener(OnContinueButton);
            howToPlayButton.onClick.AddListener(OnHowToPlayButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
            creditsButton.onClick.AddListener(OnCreditsButton);
            quitButton.onClick.AddListener(OnQuitButton);

            //How to play buttons
            backFromHowToPlayButton.onClick.AddListener(() => OnBackButton(howToPlayPanel, mainPanel));

            //Settings buttons
            videoButton.onClick.AddListener(OnVideoButton);
            audioButton.onClick.AddListener(OnAudioButton);
            controlsButton.onClick.AddListener(OnControlsButton);
            backFromSettingsButton.onClick.AddListener(() => OnBackButton(settingsPanel, mainPanel));

            //Video settings buttons
            backFromVideoButton.onClick.AddListener(() => OnBackButton(videoPanel, settingsPanel));

            //Audio settings buttons
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSlider);
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSlider);
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSlider);
            backFromAudioButton.onClick.AddListener(() => OnBackButton(audioPanel, settingsPanel));

            //Controls settings buttons
            sensitivitySlider.onValueChanged.AddListener(OnSensitivitySlider);
            backFromControlsButton.onClick.AddListener(() => OnBackButton(controlsPanel, settingsPanel));

            //Credits buttons
            backFromCreditsButton.onClick.AddListener(() => OnBackButton(creditsPanel, mainPanel));
        }

        private void Update()
        {
            if (isMainMenu) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (canvas.activeSelf) ContinueGame();
                else PauseGame();
            }
        }

        private void PauseGame()
        {
            canvas.SetActive(true);
            Time.timeScale = 0;

            //Add more pause menu logic here
        }

        private void ContinueGame()
        {
            canvas.SetActive(false);
            Time.timeScale = 1;

            ResetMenu();

            //Add more continue game logic here
        }

        private void ResetMenu()
        {
            mainPanel.gameObject.SetActive(true);

            howToPlayPanel.gameObject.SetActive(false);
            settingsPanel.gameObject.SetActive(false);
            creditsPanel.gameObject.SetActive(false);
            videoPanel.gameObject.SetActive(false);
            audioPanel.gameObject.SetActive(false);
            controlsPanel.gameObject.SetActive(false);

            //Add other panels here
        }

        private float GetAudioMixerValue(float value)
        {
            //Convert slider value to audio mixer value which is decibels (dB). End value can be adjusted.
            return Mathf.Lerp(-80f, 0f, value / 100f);
        }

        private void CommonButtonAction(GameObject panelToClose, GameObject panelToOpen)
        {
            audioSource.PlayOneShot(buttonClickSound);

            panelToClose.SetActive(false);
            panelToOpen.SetActive(true);
        }

        private void CommonSliderAction(float value, string playerPrefToUpdate, TextMeshProUGUI textToUpdate)
        {
            var currentValue = PlayerPrefs.GetInt(playerPrefToUpdate, 100);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(currentValue > value ? sliderDownSound : sliderUpSound);
            }

            var intValue = (int)(value);
            textToUpdate.text = intValue.ToString();
            PlayerPrefs.SetInt(playerPrefToUpdate, intValue);
        }

        private void OnBackButton(GameObject panelToClose, GameObject panelToOpen)
        {
            CommonButtonAction(panelToClose, panelToOpen);
        }

        private void OnStartButton()
        {
            CommonButtonAction(mainPanel, mainPanel);

            SceneManager.LoadScene("MainScene");
        }

        private void OnContinueButton()
        {
            CommonButtonAction(mainPanel, mainPanel);

            ContinueGame();
        }

        private void OnHowToPlayButton()
        {
            CommonButtonAction(mainPanel, howToPlayPanel);
        }

        private void OnSettingsButton()
        {
            CommonButtonAction(mainPanel, settingsPanel);
        }

        private void OnCreditsButton()
        {
            CommonButtonAction(mainPanel, creditsPanel);
        }

        private void OnQuitButton()
        {
            CommonButtonAction(mainPanel, mainPanel);

            Application.Quit();
        }

        private void OnAudioButton()
        {
            CommonButtonAction(settingsPanel, audioPanel);
        }

        private void OnVideoButton()
        {
            CommonButtonAction(settingsPanel, videoPanel);
        }

        private void OnControlsButton()
        {
            CommonButtonAction(settingsPanel, controlsPanel);
        }

        private void OnMasterVolumeSlider(float value)
        {
            CommonSliderAction(value, MasterVolume, masterVolumeAmountText);
            audioMixer.SetFloat(MasterVolume, GetAudioMixerValue(value));
        }

        private void OnMusicVolumeSlider(float value)
        {
            CommonSliderAction(value, MusicVolume, musicVolumeAmountText);
            audioMixer.SetFloat(MusicVolume, GetAudioMixerValue(value));
        }

        private void OnSFXVolumeSlider(float value)
        {
            CommonSliderAction(value, SFXVolume, sfxVolumeAmountText);
            audioMixer.SetFloat(SFXVolume, GetAudioMixerValue(value));
        }

        private void OnSensitivitySlider(float value)
        {
            CommonSliderAction(value, Sensitivity, sensitivityAmountText);
        }
    }
}
