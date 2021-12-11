using System;
using System.Collections;
using UnityEngine;
using Game.Core;
using Zenject;

namespace Game.Mechanics.Mob
{
    public class MobsUpdater : MonoBehaviour
    {
        [Min(1)] [SerializeField] private float _mobUpdateInterval;
        
        [Header("Mob hp")]
        [Min(225)] [SerializeField] private int _startMobHp;
        [Min(0)] [SerializeField] private int _mobHpIncrease;
        
        [Header("Mob spawn")]
        [Min(0.5f)] [SerializeField] private float _startSpawnInterval;
        [SerializeField] private float _spawnIntervalDecrease;
        [Min(0.1f)] [SerializeField] private float _minSpawnInterval;
        
        private int _currentStartMobHp;
        private float _currentSpawnInterval;
        private GameSettings _gameSettings;
        private GameManager _gameManager;

        public event Action StartMobUpdaterEvent;

        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        public int CurrentStartMobHp => _currentStartMobHp;
        public float CurrentSpawnInterval => _currentSpawnInterval;

        private void Awake()
        {
            _gameManager.StartGameEvent += StartGame;
        }

        private void StartGame()
        {
            _currentStartMobHp = _startMobHp;
            _currentSpawnInterval = _startSpawnInterval;
            StartCoroutine(UpdateMobHp());
            StartMobUpdaterEvent?.Invoke();
        }

        private IEnumerator UpdateMobHp()
        {
            while (true)
            {
                yield return new WaitForSeconds(_mobUpdateInterval);
                _currentStartMobHp += _mobHpIncrease;
                _currentSpawnInterval -= _spawnIntervalDecrease;
                if (_currentSpawnInterval <= _minSpawnInterval)
                    _currentSpawnInterval = _minSpawnInterval;
            }
        }
    }
}