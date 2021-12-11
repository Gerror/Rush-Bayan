using System;
using System.Collections.Generic;
using Game.Core;
using Game.Mechanics;
using UnityEngine;
using Game.UI;
using Zenject;

namespace Game.Mechanics
{
    public class BaseLevelUpController : MonoBehaviour
    {
        [Min(100)] [SerializeField] private int _startManaCost;
        [Min(100)] [SerializeField] private int _manaCostIncrease;
        [SerializeField] private TowerOwner _towerOwner;
        
        [SerializeField] private List<int> _manaCosts;

        private GameSettings _gameSettings;
        private GameManager _gameManager;

        public List<BaseLevelScreen> LevelScreens;
        public List<int> ManaCosts
        {
            get => _manaCosts;
        }
        
        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _manaCosts = new List<int>();
            _gameManager.StartGameEvent += StartGame;
            _gameManager.EndGameEvent += EndGame;
            _towerOwner.InputMechanics.LevelUpEvent += LevelUp;
        }

        private void StartGame()
        {
            for(int i = 0; i < _gameSettings.NumberOfTypeOfTower; i++)
            {
                _manaCosts.Add(_startManaCost);
            }
            
            for (int i = 0; i < LevelScreens.Count; i++)
            {
                LevelScreens[i].SetManaCost(_manaCosts[i].ToString());
                LevelScreens[i].SetLevel(_towerOwner.BaseLevels[i]);
            }
        }

        private void EndGame()
        {
            _manaCosts.Clear();
        }

        private void LevelUp(int towerIndex)
        {
            if (_towerOwner.ManaMechanics.CurrentMana >= _manaCosts[towerIndex])
            {
                bool result = _towerOwner.BaseLevelUp(towerIndex);
                if (result)
                {
                    _towerOwner.ManaMechanics.ChangeMana(-1 * _manaCosts[towerIndex]);
                    _manaCosts[towerIndex] += _manaCostIncrease;
                    
                    LevelScreens[towerIndex].SetLevel(_towerOwner.BaseLevels[towerIndex]);
                    
                    if (_towerOwner.BaseLevels[towerIndex] !=
                        _gameSettings.MaxLevels[(int) LevelType.BaseLevel] - 1)
                        LevelScreens[towerIndex].SetManaCost(_manaCosts[towerIndex].ToString());
                    else
                        LevelScreens[towerIndex].SetManaCost("");
                }
            }
        }
    }
}