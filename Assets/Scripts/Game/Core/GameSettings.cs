using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public enum LevelType
    {
        BaseLevel = 0,
        MergeLevel = 1,
    }
    
    public class GameSettings: MonoBehaviour
    {
        public int NumberOfTypeOfTower = 5;
        public int MaxPlayerHealth = 3;

        public int[] MaxLevels =
        {
            3,
            3
        };
    }
}
