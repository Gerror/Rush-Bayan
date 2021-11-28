using System.Collections.Generic;
using UnityEngine;
using Game.Mechanics.Mob;
using Game.Core;
using Game.Mechanics.Input;
using Game.Mechanics.Tower;

namespace Game.Mechanics
{
    public class TowerOwner : MonoBehaviour
    {
        [SerializeField] private MobSpawnMechanics _mobSpawnMechanics;
        [SerializeField] private TowerConfig[] _towerConfigs = new TowerConfig[GameSettings.NumberOfTypeOfTower];
        [SerializeField] private InputMechanics _inputMechanics;
        [SerializeField] private ManaMechanics _manaMechanics;
        [SerializeField] private TowerSpawnMechanics _towerSpawnMechanics;

        private List<GameObject>[] _towers = new List<GameObject>[GameSettings.NumberOfTypeOfTower];
        private int[] _baseLevels = new int[GameSettings.NumberOfTypeOfTower];
        
        public int[] BaseLevels => _baseLevels;
        public InputMechanics InputMechanics => _inputMechanics;
        public MobSpawnMechanics MobSpawnMechanics => _mobSpawnMechanics;
        public ManaMechanics ManaMechanics => _manaMechanics;
        public TowerConfig[] TowerConfigs => _towerConfigs;

        private void Start()
        {
            _inputMechanics.StartInputMechanics();
            for (int i = 0; i < _towers.Length; i++)
                _towers[i] = new List<GameObject>();
            _towerSpawnMechanics.TowerSpawnEvent += AddTower;
        }

        private void AddTower(int towerIndex, GameObject towerGo)
        {
            _towers[towerIndex].Add(towerGo);
        }

        public bool BaseLevelUp(int towerIndex)
        {
            if (_baseLevels[towerIndex] == GameSettings.MaxLevels[(int) GameSettings.LevelType.BaseLevel] - 1)
                return false;

            _baseLevels[towerIndex]++;

            foreach (var towerGo in _towers[towerIndex])
            {
                TowerLevels towerLevels = towerGo.GetComponent<TowerLevels>();
                towerLevels.LevelUp(GameSettings.LevelType.BaseLevel);
            }
            
            return true;
        }
    }
}
