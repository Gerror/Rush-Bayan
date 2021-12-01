using System.Collections.Generic;
using Game.Core;
using Game.Mechanics;
using UnityEngine;
using Game.UI;
using Zenject;

namespace Game.Controllers
{
    public class BaseLevelUpController : MonoBehaviour
    {
        [SerializeField] private TowerOwner _towerOwner;
        [SerializeField] private ManaCostProvider _manaCostProvider;

        private GameSettings _gameSettings;
        private GameManager _gameManager;

        public List<BaseLevelScreen> LevelScreens;

        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _gameManager.StartGameEvent += StartGame;
            _towerOwner.InputMechanics.LevelUpEvent += LevelUp;
        }

        private void StartGame()
        {
            for (int i = 0; i < LevelScreens.Count; i++)
            {
                LevelScreens[i].SetManaCost(_manaCostProvider.GetManaCost(i).ToString());
                LevelScreens[i].SetLevel(_towerOwner.BaseLevels[i]);
            }
        }

        private void LevelUp(int towerIndex)
        {
            if (_towerOwner.ManaMechanics.CurrentMana >= _manaCostProvider.GetManaCost(towerIndex))
            {
                bool result = _towerOwner.BaseLevelUp(towerIndex);
                if (result)
                {
                    _towerOwner.ManaMechanics.ChangeMana(-1 * _manaCostProvider.GetManaCost(towerIndex));
                    _manaCostProvider.IncreaseManaCost(towerIndex);
                    
                    LevelScreens[towerIndex].SetLevel(_towerOwner.BaseLevels[towerIndex]);
                    
                    if (_towerOwner.BaseLevels[towerIndex] !=
                        _gameSettings.MaxLevels[(int) LevelType.BaseLevel] - 1)
                        LevelScreens[towerIndex].SetManaCost(_manaCostProvider.GetManaCost(towerIndex).ToString());
                    else
                        LevelScreens[towerIndex].SetManaCost("");
                }
            }
        }
    }
}