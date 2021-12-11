using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using UnityEngine;
using Game.Mechanics.Tower;
using Zenject;

namespace Game.Mechanics.Input
{
    public class AiMechanics : InputMechanics
    {
        [SerializeField] private int _maxTowers = 15;
        [SerializeField] private ManaCostProvider _manaCostProvider;
        [Min(2)] [SerializeField] private int minTowersForMerge = 10;
        [Min(1)] [SerializeField] private int minTowersForLevelUp = 3;
        private TowerOwner _towerOwner;
        private GameSettings _gameSettings;
    
        [Inject]
        private void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        private void Awake()
        {
            _towerOwner = GetComponent<TowerOwner>();
        }

        public override void StartInputMechanics()
        {
            StartCoroutine(StartingActions());
            StartCoroutine(CurrentInput());
        }

        private IEnumerator StartingActions()
        {
            while (_manaMechanics.CurrentMana >= _towerSpawnMechanics.CurrentTowerPrice)
            {
                yield return new WaitForSeconds(0.2f);
                _towerSpawnMechanics.SpawnTower();
            }
        }

        private IEnumerator CurrentInput()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                (bool iCanSpawnTower, int priceSpawnTower) = CheckSpawnTower();
                (bool iCanLevelUp, int priceLevelUp, int towerIndex) = CheckLevelUp();

                if (iCanLevelUp && iCanSpawnTower)
                {
                    if (priceSpawnTower >= priceLevelUp)
                        _towerSpawnMechanics.SpawnTower();
                    else
                        LevelUp(towerIndex);
                }
                else if (iCanSpawnTower)
                    _towerSpawnMechanics.SpawnTower();
                else if (iCanLevelUp)
                    LevelUp(towerIndex);
                
                (bool iCanMergeTowers, Tower.Tower mainTower, Tower.Tower secondTower) = CheckMergeTower();
                if (iCanMergeTowers)
                {
                    mainTower.Merge(secondTower);
                }
            }
        }

        private (bool iCanSpawnTower, int price) CheckSpawnTower()
        {
            if (_manaMechanics.CurrentMana >= _towerSpawnMechanics.CurrentTowerPrice)
                return (true, _towerSpawnMechanics.CurrentTowerPrice);
            return (false, -1);
        }

        private (bool iCanLevelUp, int price, int towerIndex) CheckLevelUp()
        {
            int minLevelUpPrice = 0;
            int levelUpTowerIndex = 0;
            
            
            bool hasTowers = false;
            for (int i = 0; i < _towerOwner.Towers.Length; i++)
            {
                Dictionary<int, Tower.Tower> dictionary = _towerOwner.Towers[i];
                if (dictionary.Count >= minTowersForLevelUp || (dictionary.Count >= 1 && _towerOwner.TowerCount == _maxTowers))
                {
                    if (_towerOwner.BaseLevels[i] == _gameSettings.MaxLevels[(int) LevelType.BaseLevel] - 1)
                        continue;

                    hasTowers = true;
                    int manaCost = _manaCostProvider.GetManaCost(i);
                    
                    if (minLevelUpPrice == 0 || minLevelUpPrice > manaCost)
                    {
                        minLevelUpPrice = manaCost;
                        levelUpTowerIndex = i;
                    }
                }
            }

            if (hasTowers && _manaMechanics.CurrentMana >= minLevelUpPrice)
                return (true, minLevelUpPrice, levelUpTowerIndex);
            return (false, -1, -1);
        }

        private (bool iCanMergeTowers, Tower.Tower mainTower, Tower.Tower secondTower) CheckMergeTower()
        {
            for (int i = 0; i < _towerOwner.Towers.Length; i++)
            {
                Dictionary<int, Tower.Tower> dictionary = _towerOwner.Towers[i];
                if (dictionary.Count >= minTowersForMerge || (dictionary.Count > 1 && _towerOwner.TowerCount == _maxTowers))
                {
                    foreach (var tower1 in dictionary.Values)
                    {
                        foreach (var tower2 in dictionary.Values)
                        {
                            if (tower1.CanMerge(tower2))
                                return (true, tower1, tower2);
                        }
                    }
                }
            }

            return (false, null, null);
        }
        
    }
}