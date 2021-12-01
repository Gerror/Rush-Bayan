using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Mechanics
{
    public class ManaCostProvider : MonoBehaviour
    {
        [Min(100)] [SerializeField] private int _startManaCost;
        [Min(100)] [SerializeField] private int _manaCostIncrease;
        [SerializeField] private List<int> _manaCosts;
        private GameManager _gameManager;
        private GameSettings _gameSettings;
        
        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        public int GetManaCost(int towerIndex)
        {
            return _manaCosts[towerIndex];
        }

        public void IncreaseManaCost(int towerIndex)
        {
            _manaCosts[towerIndex] += _manaCostIncrease;
        }
        
        private void Awake()
        {
            _manaCosts = new List<int>();
            _gameManager.StartGameEvent += StartGame;
            _gameManager.EndGameEvent += EndGame;
        }

        private void StartGame()
        {
            for(int i = 0; i < _gameSettings.NumberOfTypeOfTower; i++)
            {
                _manaCosts.Add(_startManaCost);
            }
        }

        private void EndGame()
        {
            _manaCosts.Clear();
        }
    }
}