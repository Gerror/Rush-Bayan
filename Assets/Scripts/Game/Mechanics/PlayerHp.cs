using System;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Mechanics
{
    public class PlayerHp : MonoBehaviour
    {
        private int _currentHp;
        private GameSettings _gameSettings;
        private GameManager _gameManager;

        public event Action DeadEvent;

        public int CurrentHp => _currentHp;

        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _gameManager.StartGameEvent += StartGame;
        }

        private void StartGame()
        {
            _currentHp = _gameSettings.MaxPlayerHealth;
        }

        public void GetDamaged()
        {
            _currentHp--;
            if (_currentHp <= 0)
            {
                DeadEvent?.Invoke();
            }
        }
    }
}