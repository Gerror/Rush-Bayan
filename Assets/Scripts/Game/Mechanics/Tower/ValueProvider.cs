using System;
using Game.Core;
using UnityEngine;

namespace Game.Mechanics.Tower
{
    [Serializable]
    public class ValueProvider
    {
        [SerializeField] private float _baseValue;
        [SerializeField] private Modifier[] _modifiers = new Modifier[GameSettings.MaxLevels.Length];

        public Modifier[] Modifiers => _modifiers;

        public void OnValidate()
        {
            for (int i = 0; i < _modifiers.Length; i++)
            {
                _modifiers[i].OnValidate();
                _modifiers[i].LevelType = (GameSettings.LevelType) i;
            }
        }

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
        public GameSettings.LevelType LevelType;
        public float[] LevelModifiers;

        public void OnValidate()
        {
            if (LevelModifiers.Length != GameSettings.MaxLevels[(int) LevelType])
                LevelModifiers = new float[GameSettings.MaxLevels[(int) LevelType]];
        }
    }
}