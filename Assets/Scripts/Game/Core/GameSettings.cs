using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public static class GameSettings
    {
        public readonly static int NumberOfTypeOfTower = 5;
        public readonly static float MobUpdateInterval = 10f;
        
        public enum LevelType
        {
            BaseLevel = 0,
            MergeLevel = 1,
        }

        public readonly static int[] MaxLevels =
        {
            3,
            3
        };
    }
}
