using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Mechanics.Mob;
using Game.Core;
using Game.Mechanics.Input;
using Game.Mechanics.Tower;
using Zenject;

namespace Game.Mechanics
{
    public class TowerOwner : MonoBehaviour
    {
        [SerializeField] private MobSpawnMechanics _mobSpawnMechanics;
        [SerializeField] private TowerConfig[] _towerConfigs;
        [SerializeField] private InputMechanics _inputMechanics;
        [SerializeField] private ManaMechanics _manaMechanics;
        [SerializeField] private TowerSpawnMechanics _towerSpawnMechanics;
        [SerializeField] private PlayerHp _playerHp;
        
        private Dictionary<int, Tower.Tower>[] _towers;
        private int[] _baseLevels;
        private int _towerCount;
        
        private GameSettings _gameSettings;
        private GameManager _gameManager;

        public event Action<bool> DeadEvent;
        public event Action<int, bool> GetDamagedEvent;
        public bool IsPlayer = false;

        public int TowerCount => _towerCount;

        public int[] BaseLevels => _baseLevels;
        public InputMechanics InputMechanics => _inputMechanics;
        public MobSpawnMechanics MobSpawnMechanics => _mobSpawnMechanics;
        public ManaMechanics ManaMechanics => _manaMechanics;
        public TowerConfig[] TowerConfigs => _towerConfigs;
        public Dictionary<int, Tower.Tower>[] Towers => _towers;

        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _baseLevels = new int[_gameSettings.NumberOfTypeOfTower];
            _towers = new Dictionary<int, Tower.Tower>[_gameSettings.NumberOfTypeOfTower];
            for (int i = 0; i < _towers.Length; i++)
                _towers[i] = new Dictionary<int, Tower.Tower>();

            _gameManager.EndGameEvent += EndGame;
            _gameManager.StartGameEvent += StartGame;
        }

        private void EndGame()
        {
            for (int i = 0; i < _baseLevels.Length; i++)
                _baseLevels[i] = 0;
            foreach (var towerList in _towers)
            {
                towerList.Clear();
            }
        }

        private void StartGame()
        {
            _towerCount = 0;
            _inputMechanics.StartInputMechanics();
        }

        private void Start()
        {
            _towerSpawnMechanics.TowerSpawnEvent += AddTower;
            _towerSpawnMechanics.TowerMergeEvent += DeleteTower;
            _mobSpawnMechanics.MobEndMovementEvent += GetDamaged;
            _playerHp.DeadEvent += Dead;
        }

        private void Dead()
        {
            DeadEvent?.Invoke(IsPlayer);
        }
        
        private void GetDamaged()
        {
            _playerHp.GetDamaged();
            GetDamagedEvent?.Invoke(_playerHp.CurrentHp, IsPlayer);
        }

        private void AddTower(int fieldIndex, int towerIndex, Tower.Tower tower)
        {
            _towerCount++;
            _towers[towerIndex].Add(fieldIndex, tower);
        }

        private void DeleteTower(int fieldIndex, int towerIndex)
        {
            _towerCount--;
            _towers[towerIndex].Remove(fieldIndex);
        }

        public bool BaseLevelUp(int towerIndex)
        {
            if (_baseLevels[towerIndex] == _gameSettings.MaxLevels[(int) LevelType.BaseLevel] - 1)
                return false;

            _baseLevels[towerIndex]++;

            foreach (KeyValuePair<int, Tower.Tower> pair in _towers[towerIndex])
            {
                TowerLevels towerLevels = pair.Value.gameObject.GetComponent<TowerLevels>();
                towerLevels.LevelUp(LevelType.BaseLevel);
            }
            
            return true;
        }
    }
}
