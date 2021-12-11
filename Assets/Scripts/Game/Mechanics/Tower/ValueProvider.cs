using System;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Mechanics.Tower
{
    [Serializable]
    public class ValueProvider
    {
        [SerializeField] private float _baseValue;
        [SerializeField] private Modifier[] _modifiers;
        public Modifier[] Modifiers => _modifiers;
        
        public float GetValue(int[] levels)
        {
            float value = _baseValue;
            for (int i = 0; i < levels.Length; i++)
            {
                int level = levels[i];
                value += _modifiers[i].LevelModifiers[level];
            }

            return value;
        }
    }

    [Serializable]
    public class Modifier
    {
        public LevelType LevelType;
        public float[] LevelModifiers;
    }
}