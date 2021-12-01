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
        
        private Dictionary<int, GameObject>[] _towers;
        private int[] _baseLevels;
        
        private GameSettings _gameSettings;
        private GameManager _gameManager;

        public event Action DeadEvent;
        public event Action<int, bool> GetDamagedEvent;
        public bool IsPlayer = false;
        
        public int[] BaseLevels => _baseLevels;
        public InputMechanics InputMechanics => _inputMechanics;
        public MobSpawnMechanics MobSpawnMechanics => _mobSpawnMechanics;
        public ManaMechanics ManaMechanics => _manaMechanics;
        public TowerConfig[] TowerConfigs => _towerConfigs;
        
        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _baseLevels = new int[_gameSettings.NumberOfTypeOfTower];
            _towers = new Dictionary<int, GameObject>[_gameSettings.NumberOfTypeOfTower];
            for (int i = 0; i < _towers.Length; i++)
                _towers[i] = new Dictionary<int, GameObject>();

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
            DeadEvent?.Invoke();
        }
        
        private void GetDamaged()
        {
            _playerHp.GetDamaged();
            GetDamagedEvent?.Invoke(_playerHp.CurrentHp, IsPlayer);
        }

        private void AddTower(int fieldIndex, int towerIndex, GameObject towerGo)
        {
            _towers[towerIndex].Add(fieldIndex, towerGo);
        }

        private void DeleteTower(int fieldIndex, int towerIndex)
        {
            _towers[towerIndex].Remove(fieldIndex);
        }

        public bool BaseLevelUp(int towerIndex)
        {
            if (_baseLevels[towerIndex] == _gameSettings.MaxLevels[(int) LevelType.BaseLevel] - 1)
                return false;

            _baseLevels[towerIndex]++;

            foreach (KeyValuePair<int, GameObject> pair in _towers[towerIndex])
            {
                TowerLevels towerLevels = pair.Value.GetComponent<TowerLevels>();
                towerLevels.LevelUp(LevelType.BaseLevel);
            }
            
            return true;
        }
    }
}
