using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mechanics.Mob
{
    public class MobMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private MobSpawnMechanics _mobSpawnMechanics;

        private int _currentPointIndex;

        public event Action EndMovementEvent;

        public MobSpawnMechanics MobSpawnMechanics
        {
            get => _mobSpawnMechanics;
            set
            {
                if (!_mobSpawnMechanics)
                    _mobSpawnMechanics = value;
            }
        }

        private void Start()
        {
            _currentPointIndex = 1;
        }

        void Update()
        {
            if (_currentPointIndex < _mobSpawnMechanics.PointList.Count)
            {
                Vector3 direction = _mobSpawnMechanics.PointList[_currentPointIndex] - transform.position;
                if (direction.magnitude < 1e-2)
                    _currentPointIndex++;

                direction = direction.normalized * _speed;
                
                transform.Translate(direction * Time.deltaTime);
            }
            else
            {
                EndMovementEvent?.Invoke();    
            }
        }
    }
}