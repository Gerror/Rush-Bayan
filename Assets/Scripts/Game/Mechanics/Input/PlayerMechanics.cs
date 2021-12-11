using UnityEngine;
using System;
using Game.UI;

namespace Game.Mechanics.Input
{
    public class PlayerMechanics : InputMechanics
    {
        [SerializeField] private SpawnTowerUI _spawnTowerUi;
        [SerializeField] private ManaScreen _manaScreen;
        [SerializeField] private BaseLevelUpButton[] _levelUpButtons;
        
        public override void StartInputMechanics()
        {
            _levelUpButtons = FindObjectsOfType<BaseLevelUpButton>();
            foreach (var levelUpButton in _levelUpButtons)
            {
                levelUpButton.LevelUpEvent += LevelUp;
            }
            
            _spawnTowerUi.spawnTowerEvent += SpawnTower;

            _manaMechanics.changeManaEvent += SetManaValue;

            SetManaValue();
            SetPriceValue();
        }

        private void SetManaValue()
        {
            _manaScreen.SetManaValue(_manaMechanics.CurrentMana);
        }

        private void SetPriceValue()
        {
            _spawnTowerUi.SetPrice(_towerSpawnMechanics.CurrentTowerPrice);
        }
        
        private void SpawnTower()
        {
            _towerSpawnMechanics.SpawnTower();
            SetPriceValue();
        }
    }
}
