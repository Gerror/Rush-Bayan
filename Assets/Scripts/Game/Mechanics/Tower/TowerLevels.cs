using Game.Core;
using UnityEngine;

namespace Game.Mechanics.Tower
{
    public class TowerLevels : MonoBehaviour
    {
        [SerializeField] private int[] _levels = new int[GameSettings.MaxLevels.Length];
        private TowerOwner _towerOwner;
        
        public int[] Levels => _levels;

        public void SetTowerOwner(TowerOwner towerOwner)
        {
            _towerOwner = towerOwner;
        }
        
        public void LevelUp(GameSettings.LevelType levelType)
        {
            if (_levels[(int) levelType] == GameSettings.MaxLevels[(int) levelType])
                return;
            _levels[(int) levelType]++;
        }

        public void SetCurrentBaseLevel(int towerIndex)
        {
            _levels[(int) GameSettings.LevelType.BaseLevel] = _towerOwner.BaseLevels[towerIndex];
        }
    }
}