using UnityEngine;
using Game.UI;
using Game.Mechanics.Tower;

namespace Game.Mechanics
{
    [RequireComponent(typeof(TowerSpawnMechanics))]
    [RequireComponent(typeof(ManaMechanics))]
    public class PlayerMechanics : MonoBehaviour
    {
        [SerializeField] private SpawnTowerUI _spawnTowerUi;
        [SerializeField] private ManaScreen _manaScreen;
        
        private TowerSpawnMechanics _towerSpawnMechanics;
        private ManaMechanics _manaMechanics;
        
        private void OnValidate()
        {
            if (!_towerSpawnMechanics)
                _towerSpawnMechanics = GetComponent<TowerSpawnMechanics>();
            if (!_manaMechanics)
                _manaMechanics = GetComponent<ManaMechanics>();
        }

        void Start()
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
