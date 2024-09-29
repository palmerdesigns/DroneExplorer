using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menus : MonoBehaviour
{
    #region UI Elements Fields
    private UIDocument _document;
    private Button _playButton;
    private Button _settingsButton;
    private Button _settingsBackButton;
    private Button _settingsCloseButton;
    private Button _controlsButton;
    private Button _controlsBackButton;
    private Button _controlsCloseButton;
    private Label _musicLevel;
    private Label _sfxLevel;

    // Audio Buttons

    private Button _musicAdd;
    private Button _musicSubtract;
    private Button _sfxAdd;
    private Button _sfxSubtract;

    private VisualElement _pauseMenu;
    private VisualElement _settingsMenu;
    private VisualElement _mainMenu; // TODO: Add MainMenu
    private VisualElement _controlsMenu;

    #endregion

    #region Fields

    private AudioSource _audioSource;
    public AudioSource _gameManagerAudio;
    private SimpleMovement _simpleMovement;
    public bool isPaused = false;
    [SerializeField]
    MenuState _menuState;

    public enum MenuState
    {
        MainMenu,
        PauseMenu,
        SettingsMenu,
        Controls,
        None
    }

    private List<Button> _menuButtons = new List<Button>();

    #endregion

    private void Awake()
    {
        //Get Components

        _audioSource = GetComponent<AudioSource>();
        _simpleMovement = FindObjectOfType<SimpleMovement>();
        _document = GetComponent<UIDocument>();
        _musicLevel = _document.rootVisualElement.Q<Label>("CurrentMusicVolume") as Label;
        _sfxLevel = _document.rootVisualElement.Q<Label>("CurrentSFXVolume") as Label;

        //Events

        _playButton = _document.rootVisualElement.Q<Button>("StartGameButton") as Button;
        _playButton.RegisterCallback<ClickEvent>(OnPlayGameClicked);

        _pauseMenu = _document.rootVisualElement.Q<VisualElement>("PauseMenu");
        _settingsMenu = _document.rootVisualElement.Q<VisualElement>("SettingsMenu");
        _controlsMenu = _document.rootVisualElement.Q<VisualElement>("ControlsMenu");

        _settingsButton = _document.rootVisualElement.Q<Button>("SettingsButton") as Button;
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);

        _settingsBackButton = _document.rootVisualElement.Q<Button>("SettingsBackButton") as Button;
        _settingsBackButton.RegisterCallback<ClickEvent>(OnSettingsBack);

        _settingsCloseButton = _document.rootVisualElement.Q<Button>("SettingsCloseButton") as Button;
        _settingsCloseButton.RegisterCallback<ClickEvent>(OnSettingsCloses);

        _controlsButton = _document.rootVisualElement.Q<Button>("ControlsButton") as Button;
        _controlsButton.RegisterCallback<ClickEvent>(OnControlsClicked);

        _controlsBackButton = _document.rootVisualElement.Q<Button>("ControlsBackButton") as Button;
        _controlsBackButton.RegisterCallback<ClickEvent>(OnSettingsBack);

        _controlsCloseButton = _document.rootVisualElement.Q<Button>("ControlsCloseButton") as Button;
        _controlsCloseButton.RegisterCallback<ClickEvent>(OnSettingsCloses);

        /// <summary>
        /// Audio Buttons
        /// </summary>
        /// 

        //Music Buttons

        _musicAdd = _document.rootVisualElement.Q<Button>("MusicLevelAdd") as Button;
        _musicAdd.RegisterCallback<ClickEvent>(OnMusicLevelIncreased);

        _musicSubtract = _document.rootVisualElement.Q<Button>("MusicLevelSubtract") as Button;
        _musicSubtract.RegisterCallback<ClickEvent>(OnMusicLevelDecreased);

        //SFX Buttons

        _sfxAdd = _document.rootVisualElement.Q<Button>("SFXLevelAdd") as Button;
        _sfxAdd.RegisterCallback<ClickEvent>(OnSFXLevelIncreased);

        _sfxSubtract = _document.rootVisualElement.Q<Button>("SFXLevelSubtract") as Button;
        _sfxSubtract.RegisterCallback<ClickEvent>(OnSFXLevelDecreased);


        // Play Sound for each button

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnDisable()
    {
        // Unregister Events

        _playButton.UnregisterCallback<ClickEvent>(OnPlayGameClicked);
        _pauseMenu.UnregisterCallback<ClickEvent>(OnSettingsClicked);
        _settingsMenu.UnregisterCallback<ClickEvent>(OnSettingsBack);
        _settingsMenu.UnregisterCallback<ClickEvent>(OnSettingsCloses);
        _controlsMenu.UnregisterCallback<ClickEvent>(OnControlsClicked);
        _controlsMenu.UnregisterCallback<ClickEvent>(OnControlsBack);
        _controlsMenu.UnregisterCallback<ClickEvent>(OnControlsCloses);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void Update()
    {
        CheckMenuState();
    }

    #region Buttons Click Events

    private void OnPlayGameClicked(ClickEvent evt)
    {
        TogglePause();
        Debug.Log("Play Game Button Clicked");
    }

    private void OnSettingsClicked(ClickEvent evt)
    {
        _menuState = MenuState.SettingsMenu;
    }

    private void OnSettingsBack(ClickEvent evt)
    {
        _menuState = MenuState.PauseMenu;
    }

    private void OnSettingsCloses(ClickEvent evt)
    {
        TogglePause();
        _menuState = MenuState.None;
    }

    //Generic Method for all buttons
    private void OnAllButtonClick(ClickEvent click)
    {
        Debug.Log("All Button Click, Play Sound");
        _audioSource.Play();
    }

    private void OnControlsClicked(ClickEvent evt)
    {
        _menuState = MenuState.Controls;
    }

    private void OnControlsBack(ClickEvent evt)
    {
        _menuState = MenuState.PauseMenu;
    }

    private void OnControlsCloses(ClickEvent evt)
    {
        TogglePause();
        _menuState = MenuState.None;
    }

    private void OnMusicLevelIncreased(ClickEvent evt)
    {
        Debug.Log("Increased");
        _gameManagerAudio.volume = Mathf.Clamp(_gameManagerAudio.volume + 0.1f, 0, 1);
        UpdateMusicLevelLabel();
    }

    private void OnMusicLevelDecreased(ClickEvent evt)
    {
        Debug.Log("Decreased");
        _gameManagerAudio.volume = Mathf.Clamp(_gameManagerAudio.volume - 0.1f, 0, 1);
        UpdateMusicLevelLabel();
    }

    private void OnSFXLevelIncreased(ClickEvent evt)
    {
        Debug.Log("Increased");
        UpdateSFXVolume(0.1f);
        UpdateSFXLevelLabel();
    }

    private void OnSFXLevelDecreased(ClickEvent evt)
    {
        Debug.Log("Decreased");
        UpdateSFXVolume(-0.1f);
        UpdateSFXLevelLabel();
    }

    /// <summary>
    /// Updates the music level label to reflect the current volume as a whole number.
    /// </summary>
    public void UpdateMusicLevelLabel()
    {
        int wholeNumberVolume = Mathf.RoundToInt(_gameManagerAudio.volume * 10);
        _musicLevel.text = wholeNumberVolume.ToString();
    }

    public void UpdateSFXLevelLabel()
    {
        int wholeNumberVolume = Mathf.RoundToInt(_audioSource.volume * 10);
        _sfxLevel.text = wholeNumberVolume.ToString();
    }

    /// <summary>
    /// Updates the volume of all AudioSources with the tag "SFX".
    /// </summary>
    /// <param name="delta">The amount to change the volume by.</param>
    private void UpdateSFXVolume(float delta)
    {
        GameObject[] sfxObjects = GameObject.FindGameObjectsWithTag("SFX");
        foreach (GameObject sfxObject in sfxObjects)
        {
            AudioSource audioSource = sfxObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = Mathf.Clamp(audioSource.volume + delta, 0, 1);
            }
        }
    }

    #endregion

    #region Managing States

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (_simpleMovement != null)
        {
            _simpleMovement.UpdateCursorState();
        }

        _menuState = isPaused ? MenuState.PauseMenu : MenuState.None;
    }

    public void CheckMenuState()
    {
        switch (_menuState)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.PauseMenu:
                _document.rootVisualElement.style.display = DisplayStyle.Flex;
                _pauseMenu.style.display = DisplayStyle.Flex;
                _settingsMenu.style.display = DisplayStyle.None;
                _controlsMenu.style.display = DisplayStyle.None;
                break;
            case MenuState.SettingsMenu:
                _settingsMenu.style.display = DisplayStyle.Flex;
                _pauseMenu.style.display = DisplayStyle.None;
                _controlsMenu.style.display = DisplayStyle.None;
                break;
            case MenuState.Controls:
                _controlsMenu.style.display = DisplayStyle.Flex;
                _pauseMenu.style.display = DisplayStyle.None;
                _settingsMenu.style.display = DisplayStyle.None;
                break;
            case MenuState.None:
                _document.rootVisualElement.style.display = DisplayStyle.None;
                break;
        }
    }

    #endregion

}
