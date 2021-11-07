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
    [RequireComponent(typeof(TowerOwnerMechanics))]
    public class TowerSpawnMechanics : MonoBehaviour
    {
        [SerializeField] private GameObject _gameField;
           
        [Min(50)] [SerializeField] private int _startTowerPrice;
        [Min(10)] [SerializeField] private int _priceIncrease;

        private ManaMechanics _manaMechanics;
        private MobSpawnMechanics _mobSpawnMechanics;
        private TowerOwnerMechanics _towerOwnerMechanics;
        
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
            if (!_towerOwnerMechanics)
                _towerOwnerMechanics = GetComponent<TowerOwnerMechanics>();
        }

        private void Awake()
        {
            _currentTowerPrice = _startTowerPrice;
            
            _mobSpawnMechanics = _towerOwnerMechanics.MobSpawnMechanics;
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
                    _towerOwnerMechanics.TowerPrefab[Random.Range(0, _towerOwnerMechanics.TowerPrefab.Length - 1)],
                    _gameField.transform.GetChild(fieldIndex));
                
                tower.GetComponent<TowerAttackMechanics>().MobSpawnMechanics = _mobSpawnMechanics;
                
                _freeFieldMap.Remove(fieldIndex);
            }
        }
    }
}