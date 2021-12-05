using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using Game.Mechanics.Tower.Attack;
using Game.Mechanics.Mob;
using Game.UI;
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
        private Dictionary<int, GameObject> _busyFieldMap;
        
        // fieldIndex, towerIndex, Tower добавленной башни
        public event Action<int, int, Tower> TowerSpawnEvent;
        // fieldIndex, towerIndex исчезающей башни
        public event Action<int, int> TowerMergeEvent; 
        
        [Inject]
        private void Construct(GameManager gameManager, PrefabFactory prefabFactory)
        {
            _gameManager = gameManager;
            _prefabFactory = prefabFactory;
        }

        public int CurrentTowerPrice
        {
            get => _currentTowerPrice;
        }

        private void Awake()
        {
            _manaMechanics = GetComponent<ManaMechanics>();
            _towerOwner = GetComponent<TowerOwner>();
            _mobSpawnMechanics = _towerOwner.MobSpawnMechanics;

            _currentTowerPrice = _startTowerPrice;
            _freeFieldMap = new Dictionary<int, bool>();
            _busyFieldMap = new Dictionary<int, GameObject>();

            _gameManager.StartGameEvent += StartGame;
            _gameManager.EndGameEvent += EndGame;
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

        private void EndGame()
        {
            foreach (KeyValuePair<int, GameObject> pair  in _busyFieldMap)
            {
                Destroy(pair.Value);
            }
            _busyFieldMap.Clear();
        }

        public void SpawnTower()
        {
            if (_freeFieldMap.Count > 0)
            {
                int fieldIndex = _freeFieldMap.Keys.ToList()[Random.Range(0, _freeFieldMap.Count)];
                SpawnTower(0, fieldIndex);
            }
        }

        private void SpawnTower(int mergeLevel, int fieldIndex, bool isMerge = false)
        {
            if (!isMerge)
            {
                if (!_manaMechanics.ChangeMana(-1 * _currentTowerPrice))
                    return;
                _currentTowerPrice += _priceIncrease;
            }

            int towerIndex = Random.Range(0, _towerOwner.TowerConfigs.Length);

            GameObject towerGo = _prefabFactory.Spawn(_towerOwner.TowerConfigs[towerIndex].Prefab, 
                _gameField.transform.GetChild(fieldIndex)); 
                
            Tower tower = towerGo.GetComponent<Tower>();
            tower.Init(_towerOwner, _mobSpawnMechanics, _manaMechanics, mergeLevel, fieldIndex, towerIndex);
            tower.MergeEvent += MergeTowers;

            GameObject towerMergeLevelView = _prefabFactory.Spawn(
                _towerOwner.TowerConfigs[towerIndex].GetMergeView(mergeLevel),
                towerGo.transform);
            
            TowerSpawnEvent?.Invoke(fieldIndex, towerIndex, tower);
            _freeFieldMap.Remove(fieldIndex);
            _busyFieldMap.Add(fieldIndex, towerGo);
        }

        private void MergeTowers(int fieldMainTower, int fieldSecondTower, int towerIndex, int currentMergeLevel)
        {
            DeleteTower(fieldMainTower, towerIndex);
            DeleteTower(fieldSecondTower, towerIndex);
            SpawnTower(currentMergeLevel + 1, fieldMainTower, true);
        }

        private void DeleteTower(int fieldIndex, int towerIndex)
        {
            _freeFieldMap.Add(fieldIndex, true);
            Destroy(_busyFieldMap[fieldIndex]);
            _busyFieldMap.Remove(fieldIndex);
            TowerMergeEvent?.Invoke(fieldIndex, towerIndex);
        }
    }
}