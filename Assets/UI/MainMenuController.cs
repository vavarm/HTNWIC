using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _playButton;
    private Button _settingsButton;
    private Button _quitButton;
    private Button _muteButton;

    private VisualElement _buttonsWrapper;

    [SerializeField] private VisualTreeAsset _settingsButtonsTemplate;
    private VisualElement _settingsButtons;
    
    [Header("Mute Button")] [SerializeField]
    private bool _muted;
    
    
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        
        // PlayButton
        _playButton = _doc.rootVisualElement.Q<Button>("PlayButton");
        _playButton.clicked += PlayButtonOnClicked; // () => { DoSomething(); }
         
        // SettingsButton
        _settingsButton = _doc.rootVisualElement.Q<Button>("SettingsButton");
        _buttonsWrapper = _doc.rootVisualElement.Q<VisualElement>("Buttons");
        _settingsButton.clicked += SettingsButtonOnClicked;
        
        
        
        // QuitButton
        _quitButton = _doc.rootVisualElement.Q<Button>("QuitButton");
        _quitButton.clicked += ExitButtonOnClicked; 
        
        // MuteButton
        _muteButton = _doc.rootVisualElement.Q<Button>("MuteButton");
        _muteButton.clicked += MuteButtonOnClicked;
        
    }
    
    private void PlayButtonOnClicked()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    
    private void SettingsButtonOnClicked()
    {
        _buttonsWrapper.Clear();
    }
    
    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }
    
    private void MuteButtonOnClicked()
    {
        _muted = !_muted;
        
        AudioListener.volume = _muted ? 0 : 1;
    }
}
