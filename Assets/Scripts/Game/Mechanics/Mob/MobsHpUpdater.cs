using System;
using System.Collections;
using UnityEngine;
using Game.Core;
using Zenject;

namespace Game.Mechanics.Mob
{
    public class MobsHpUpdater : MonoBehaviour
    {
        [Min(225)] [SerializeField] private int _startMobHp;
        private int _currentCurrentStartMobHp;
        private GameSettings _gameSettings;
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameSettings gameSettings, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _gameManager = gameManager;
        }

        public int CurrentStartMobHp
        {
            get => _currentCurrentStartMobHp;
        }

        private void Awake()
        {
            _gameManager.StartGameEvent += StartGame;
        }

        private void StartGame()
        {
            _currentCurrentStartMobHp = _startMobHp;
            StartCoroutine(UpdateMobHp());
        }

        private IEnumerator UpdateMobHp()
        {
            while (true)
            {
                yield return new WaitForSeconds(_gameSettings.MobUpdateInterval);
                _currentCurrentStartMobHp += 115;
            }
        }
    }
}