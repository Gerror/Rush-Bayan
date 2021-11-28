using System;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Mechanics
{
    public class ManaMechanics : MonoBehaviour
    {
        [Min(100)] [SerializeField] private int _startMana;

        [SerializeField] private int _currentMana = 0;

        private GameManager _gameManager;
        public event Action changeManaEvent;
        public event Action<int> changeManaToEvent;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public int CurrentMana
        {
            get => _currentMana;
        }

        private void Awake()
        {
            _currentMana = _startMana;
            _gameManager.EndGameEvent += EndGame;
        }

        private void EndGame()
        {
            _currentMana = _startMana;
        }

        public bool ChangeMana(int changeManaTo)
        {
            if (_currentMana + changeManaTo < 0)
                return false;
            _currentMana += changeManaTo;
            changeManaEvent?.Invoke();
            changeManaToEvent?.Invoke(changeManaTo);
            return true;
        }
    }
}
