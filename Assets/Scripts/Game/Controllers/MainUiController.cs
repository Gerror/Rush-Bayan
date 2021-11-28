using System;
using Game.Core;
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

        public event Action StartGameEvent;
        
        private GameManager _gameManager;
        private GameSettings _gameSettings;
        
        [Inject]
        private void Construct(GameManager gameManager, GameSettings gameSettings)
        {
            _gameManager = gameManager;
            _gameSettings = gameSettings;
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
            _endGameWindow.gameObject.SetActive(true);
            
            _gameWindow.SetActive(false);
            _gameElements.SetActive(false);
        }

        private void OpenAboutMe()
        {
            _aboutMeWindow.gameObject.SetActive(true);
            _mainMenuWindow.gameObject.SetActive(false);
        }

        private void OpenMainMenu()
        {
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
    }
}