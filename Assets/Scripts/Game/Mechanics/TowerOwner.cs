using System;
using UnityEngine;
using Game.Mechanics.Mob;
using Game.Core;
using Game.Mechanics.Input;

namespace Game.Mechanics
{
    public class TowerOwner : MonoBehaviour
    {
        [SerializeField] private MobSpawnMechanics _mobSpawnMechanics;
        [SerializeField] private TowerConfig[] _towers = new TowerConfig[GameSettings.NumberOfTypeOfTower];
        [SerializeField] private InputMechanics _inputMechanics;
        
        public MobSpawnMechanics MobSpawnMechanics
        {
            get => _mobSpawnMechanics;
        }
        
        public TowerConfig[] TowerConfigs
        {
            get => _towers;
        }

        private void Start()
        {
            _inputMechanics.StartInputMechanics();
        }
    }
}
