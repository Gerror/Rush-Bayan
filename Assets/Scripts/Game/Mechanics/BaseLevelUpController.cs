using System.Collections.Generic;
using Game.Core;
using Game.Mechanics;
using UnityEngine;
using Game.UI;

namespace Game.Mechanics
{
    public class BaseLevelUpController : MonoBehaviour
    {
        [Min(100)] [SerializeField] private int _startManaCost;
        [Min(100)] [SerializeField] private int _manaCostIncrease;
        [SerializeField] private TowerOwner _towerOwner;
        
        [SerializeField] private List<int> _manaCosts;

        public List<BaseLevelScreen> LevelScreens;
        public List<int> ManaCosts
        {
            get => _manaCosts;
        }

        private void Start()
        {
            _manaCosts = new List<int>();
            for(int i = 0; i < GameSettings.NumberOfTypeOfTower; i++)
            {
                _manaCosts.Add(_startManaCost);
            }
            
            _towerOwner.InputMechanics.LevelUpEvent += LevelUp;

            for (int i = 0; i < LevelScreens.Count; i++)
            {
                LevelScreens[i].SetManaCost(_manaCosts[i].ToString());
                LevelScreens[i].SetLevel(_towerOwner.BaseLevels[i]);
            }
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
                        GameSettings.MaxLevels[(int) GameSettings.LevelType.BaseLevel] - 1)
                        LevelScreens[towerIndex].SetManaCost(_manaCosts[towerIndex].ToString());
                    else
                        LevelScreens[towerIndex].SetManaCost("");
                }
            }
            else
            {
                Debug.Log("ERROR LEVEL UP");
            }
        }
    }
}