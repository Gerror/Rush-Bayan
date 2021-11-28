using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using Game.Mechanics.Tower.Attack;
using Game.Mechanics.Mob;
using UnityEngine;
using Zenject;
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
        [Min(0)]  [SerializeField] private int _currentTowerPrice = 0;
        
        private ManaMechanics _manaMechanics;
        private MobSpawnMechanics _mobSpawnMechanics;
        private TowerOwner _towerOwner;
        private GameManager _gameManager;
        private PrefabFactory _prefabFactory;
        
        private Dictionary<int, bool> _freeFieldMap;
    
        [Inject]
        private void Construct(GameManager gameManager, PrefabFactory prefabFactory)
        {
            _gameManager = gameManager;
            _prefabFactory = prefabFactory;
        }
        
        public event Action<int, GameObject> TowerSpawnEvent; 

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

            _gameManager.StartGameEvent += StartGame;
        }

        private void StartGame()
        {
            _currentTowerPrice = _startTowerPrice;
                
            _freeFieldMap.Clear();
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

                int towerIndex = Random.Range(0, _towerOwner.TowerConfigs.Length);

                GameObject tower = _prefabFactory.Spawn(_towerOwner.TowerConfigs[towerIndex].Prefab, 
                    _gameField.transform.GetChild(fieldIndex)); 

                TowerLevels towerLevels = tower.GetComponent<TowerLevels>();
                towerLevels.SetTowerOwner(_towerOwner);
                towerLevels.SetCurrentBaseLevel(towerIndex);
                
                TowerAttackMechanics towerAttackMechanics = tower.GetComponent<TowerAttackMechanics>();
                if (towerAttackMechanics)
                    towerAttackMechanics.MobSpawnMechanics = _mobSpawnMechanics;

                ManaExtractor manaExtractor = tower.GetComponent<ManaExtractor>();
                if (manaExtractor)
                    manaExtractor.ManaMechanics = _manaMechanics;

                
                TowerSpawnEvent?.Invoke(towerIndex, tower);
                _freeFieldMap.Remove(fieldIndex);
            }
        }
    }
}