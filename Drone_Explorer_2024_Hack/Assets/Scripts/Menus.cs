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

    // Audio Buttons

    private Button _musicAdd;
    private Button _musicSubtract;
    private Button _sfxAdd;
    private Button _sfxSubtract;

    private VisualElement _pauseMenu;

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

        //Events

        _playButton = _document.rootVisualElement.Q<Button>("StartGameButton") as Button;
        _playButton.RegisterCallback<ClickEvent>(OnPlayGameClicked);

        _pauseMenu = _document.rootVisualElement.Q<VisualElement>("PauseMenu");

        _settingsButton = _document.rootVisualElement.Q<Button>("SettingsButton") as Button;
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);

        _settingsBackButton = _document.rootVisualElement.Q<Button>("SettingsBackButton") as Button;
        _settingsBackButton.RegisterCallback<ClickEvent>(OnSettingsBack);

        _settingsCloseButton = _document.rootVisualElement.Q<Button>("SettingsCloseButton") as Button;
        _settingsCloseButton.RegisterCallback<ClickEvent>(OnSettingsCloses);

        // Audio Buttons

        

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
        //Hide Pause Menuss
        _pauseMenu.style.display = DisplayStyle.None;
        _menuState = MenuState.SettingsMenu;

    }

    private void OnSettingsBack(ClickEvent evt)
    {
        //Show Pause Menu
        _pauseMenu.style.display = DisplayStyle.Flex;
        _menuState = MenuState.PauseMenu;
        Debug.Log("Settings Back Button Clicked");
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

    private void OnMusicLevelIncreased(ChangeEvent<float> evt)
    {
       /* Debug.Log($"Slider Value Changed: {evt.newValue}");
        _gameManagerAudio.volume = evt.newValue;*/
    }

    private void OnMusicLevelDecreased(ChangeEvent<float> evt)
    {
       /* Debug.Log($"Slider Value Changed: {evt.newValue}");
        _gameManagerAudio.volume = evt.newValue;*/
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

        //change menu state based on pause state
        _menuState = isPaused ? MenuState.PauseMenu : MenuState.None;

    }

    public void CheckMenuState()
    {
        _menuState = isPaused ? MenuState.PauseMenu : MenuState.None;

        switch (_menuState)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.PauseMenu:
                _document.rootVisualElement.style.display = DisplayStyle.Flex;
                break;
            case MenuState.SettingsMenu:
                break;
            case MenuState.None:
                _document.rootVisualElement.style.display = DisplayStyle.None;
                break;
        }
    }

    #endregion

}
