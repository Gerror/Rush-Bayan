using System.Collections.Generic;
using System.Linq;
using Game.Mechanics.Tower.Attack;
using Game.Mechanics.Mob;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Mechanics.Tower
{
    [RequireComponent(typeof(ManaMechanics))]
    [RequireComponent(typeof(TowerOwner))]
    public class TowerSpawnMechanics : MonoBehaviour
    {
        [SerializeField] private GameObject _gameField;
           
        [Min(10)] [SerializeField] private int _startTowerPrice;
        [Min(10)] [SerializeField] private int _priceIncrease;

        private ManaMechanics _manaMechanics;
        private MobSpawnMechanics _mobSpawnMechanics;
        private TowerOwner _towerOwner;
        
        [SerializeField] private int _currentTowerPrice = 0;
        private Dictionary<int, bool> _freeFieldMap;

        public int CurrentTowerPrice
        {
            get => _currentTowerPrice;
        }

        private void OnValidate()
        {
            if (!_manaMechanics)
                _manaMechanics = GetComponent<ManaMechanics>();
            if (!_towerOwner)
                _towerOwner = GetComponent<TowerOwner>();
        }

        private void Awake()
        {
            _currentTowerPrice = _startTowerPrice;
            
            _mobSpawnMechanics = _towerOwner.MobSpawnMechanics;
            _freeFieldMap = new Dictionary<int, bool>();

            int i = 0;
            foreach (Transform field in _gameField.transform)
            {
                _freeFieldMap.Add(i, true);
                i++;
            }
        }

        public void SpawnTower()
        {
            if (_freeFieldMap.Count > 0)
            {
                if (!_manaMechanics.ChangeMana(-1 * _currentTowerPrice))
                    return;
                _currentTowerPrice += _priceIncrease;
                
                int fieldIndex = _freeFieldMap.Keys.ToList()[Random.Range(0, _freeFieldMap.Count)];

                GameObject tower = Object.Instantiate(
                    _towerOwner.TowerConfigs[Random.Range(0, _towerOwner.TowerConfigs.Length - 1)].Prefab,
                    _gameField.transform.GetChild(fieldIndex));

                TowerAttackMechanics towerAttackMechanics = tower.GetComponent<TowerAttackMechanics>();
                if (towerAttackMechanics)
                    towerAttackMechanics.MobSpawnMechanics = _mobSpawnMechanics;
                
                _freeFieldMap.Remove(fieldIndex);
            }
        }
    }
}