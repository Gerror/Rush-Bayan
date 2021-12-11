using System;
using UnityEngine;
using Game.Mechanics.Mob;
using Game.Core;
using Game.Mechanics.Input;

namespace Game.Mechanics
{
    public class TowerOwnerMechanics : MonoBehaviour
    {
        [SerializeField] private MobSpawnMechanics _mobSpawnMechanics;
        [SerializeField] private GameObject[] _towerPrefabs = new GameObject[GameSettings.NumberOfTypeOfTower];
        [SerializeField] private InputMechanics _inputMechanics;
        
        public MobSpawnMechanics MobSpawnMechanics
        {
            get => _mobSpawnMechanics;
        }
        
        public GameObject[] TowerPrefab
        {
            get => _towerPrefabs;
        }

        private void Start()
        {
            _inputMechanics.StartInputMechanics();
        }
    }
}
