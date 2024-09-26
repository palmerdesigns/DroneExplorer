using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menus : MonoBehaviour
{
    private AudioSource _audioSource;
    private UIDocument _document;
    private Button _button;
    private SimpleMovement _simpleMovement;
    public bool isPaused = false;
    [SerializeField]
    MenuState _menuState;

    public enum MenuState
    {
        MainMenu,
        PauseMenu,
        OptionsMenu,
        None
    }

    private List<Button> _menuButtons = new List<Button>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q<Button>("StartGameButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameClicked);
        _simpleMovement = FindObjectOfType<SimpleMovement>();

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClicked);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void Update()
    {
        CheckMenuState();
    }

    #region Methods

    private void OnPlayGameClicked(ClickEvent evt)
    {
        TogglePause();
        Debug.Log("Play Game Button Clicked");
    }

    //Generic Method for all buttons
    private void OnAllButtonClick(ClickEvent click)
    {
        Debug.Log("All Button Click, Play Sound");
        _audioSource.Play();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (_simpleMovement != null)
        {
            _simpleMovement.UpdateCursorState();
        }
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
            case MenuState.OptionsMenu:
                break;
            case MenuState.None:
                _document.rootVisualElement.style.display = DisplayStyle.None;
                break;
        }
    }

    #endregion
}
