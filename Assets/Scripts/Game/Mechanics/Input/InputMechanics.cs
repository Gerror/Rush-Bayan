using System;
using Game.Mechanics.Tower;
using UnityEngine;

namespace Game.Mechanics.Input
{
    [RequireComponent(typeof(TowerSpawnMechanics))]
    [RequireComponent(typeof(ManaMechanics))]
    public abstract class InputMechanics : MonoBehaviour
    {
        [SerializeField] protected TowerSpawnMechanics _towerSpawnMechanics;
        [SerializeField] protected ManaMechanics _manaMechanics;

        public event Action<int> LevelUpEvent;

        protected void OnValidate()
        {
            if (!_towerSpawnMechanics)
                _towerSpawnMechanics = GetComponent<TowerSpawnMechanics>();
            if (!_manaMechanics)
                _manaMechanics = GetComponent<ManaMechanics>();
        }
        
        protected void LevelUp(int towerIndex)
        {
            LevelUpEvent?.Invoke(towerIndex);
        }

        public abstract void StartInputMechanics();
    }
}