using UnityEngine;
using Game.UI;
using Game.Mechanics.Tower;

namespace Game.Mechanics.Input
{
    public class PlayerMechanics : InputMechanics
    {
        [SerializeField] private SpawnTowerUI _spawnTowerUi;
        [SerializeField] private ManaScreen _manaScreen;

        public override void StartInputMechanics()
        {
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
