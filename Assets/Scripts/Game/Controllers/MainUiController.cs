using System;
using Game.Core;
using Game.Core.Sounds;
using Game.Helpers;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Controllers
{
    public class MainUiController : MonoBehaviour
    {
        [SerializeField] private AboutMeWindow _aboutMeWindow;
        [SerializeField] private MainMenuWindow _mainMenuWindow;
        [SerializeField] private EndGameWindow _endGameWindow;
        [SerializeField] private GameObject _gameWindow;
        [SerializeField] private GameObject _gameElements;
        [SerializeField] private PlayerHpScreen _playerHpScreen;
        [SerializeField] private PlayerHpScreen _enemyHpScreen;
        [SerializeField] private AudioClip _clickAudio;
        public event Action StartGameEvent;
        
        private GameManager _gameManager;
        private GameSettings _gameSettings;
        private SoundManager _soundManager;
        
        [Inject]
        private void Construct(GameManager gameManager, GameSettings gameSettings, SoundManager soundManager)
        {
            _gameManager = gameManager;
            _gameSettings = gameSettings;
            _soundManager = soundManager;
        }

        private void Start()
        {
            OpenMainMenu();

            _mainMenuWindow.StartGameEvent += StartGame;
            _mainMenuWindow.AboutMeEvent += OpenAboutMe;
            _mainMenuWindow.ExitGameEvent += ExitHelper.Exit;

            _endGameWindow.RestartEvent += StartGame;
            _endGameWindow.BackToMainMenuEvent += OpenMainMenu;

            _aboutMeWindow.ExitEvent += OpenMainMenu;

            _gameManager.EndGameEvent += EndGame;
        }

        private void StartGame()
        {
            PlayClickAudio();
            _gameWindow.SetActive(true);
            _gameElements.SetActive(true);
            _mainMenuWindow.gameObject.SetActive(false);
            _endGameWindow.gameObject.SetActive(false);
            
            _playerHpScreen.SetHealth(_gameSettings.MaxPlayerHealth);
            _enemyHpScreen.SetHealth(_gameSettings.MaxPlayerHealth);
            
            StartGameEvent?.Invoke();
        }

        private void EndGame()
        {
            PlayClickAudio();
            _endGameWindow.gameObject.SetActive(true);
            
            _gameWindow.SetActive(false);
            _gameElements.SetActive(false);
        }

        private void OpenAboutMe()
        {
            PlayClickAudio();
            _aboutMeWindow.gameObject.SetActive(true);
            _mainMenuWindow.gameObject.SetActive(false);
        }

        private void OpenMainMenu()
        {
            PlayClickAudio();
            _mainMenuWindow.gameObject.SetActive(true);
            
            _aboutMeWindow.gameObject.SetActive(false);
            _endGameWindow.gameObject.SetActive(false);
            _gameWindow.SetActive(false);
            _gameElements.SetActive(false);
        }

        public void SetHealth(int health, bool isPlayer)
        {
            if (isPlayer)
                _playerHpScreen.SetHealth(health);
            else
                _enemyHpScreen.SetHealth(health);
        }

        public void SetEndGameText(bool deadPlayer)
        {
            if (deadPlayer)
                _endGameWindow.SetEndGameText("Поражение!");
            else
                _endGameWindow.SetEndGameText("Победа!");
        }
        
        private void PlayClickAudio()
        {
            _soundManager.CreateSoundObject().Play(_clickAudio, transform.position, false, 0.25f);
        }
    }
}