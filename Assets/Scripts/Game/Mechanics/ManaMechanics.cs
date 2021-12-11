using System;
using UnityEngine;

namespace Game.Mechanics
{
    public class ManaMechanics : MonoBehaviour
    {
        [Min(100)] [SerializeField] private int _startMana;

        [SerializeField] private int _currentMana = 0;
        public event Action changeManaEvent;
        public event Action<int> changeManaToEvent;

        public int CurrentMana
        {
            get => _currentMana;
        }

        private void Awake()
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
