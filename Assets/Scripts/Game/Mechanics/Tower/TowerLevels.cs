using System;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Mechanics.Tower
{
    public class TowerLevels : MonoBehaviour
    {
        [SerializeField] private int[] _levels;
        private GameSettings _gameSettings;
        private Animator _animator;
        
        public int[] Levels => _levels;
        
        [Inject]
        private void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void LevelUp(LevelType levelType)
        {
            if (_levels[(int) levelType] == _gameSettings.MaxLevels[(int) levelType])
                return;
            _levels[(int) levelType]++;
            
            if (levelType == LevelType.BaseLevel)
                _animator.SetTrigger("BaseLevelUp");
        }

        public void SetCurrentLevel(LevelType levelType, int value)
        {
            _levels[(int) levelType] = value;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
    }
}