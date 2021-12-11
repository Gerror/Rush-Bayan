using System.Collections;
using UnityEngine;

namespace Game.Mechanics.Tower
{
    [RequireComponent(typeof(TowerLevels))]
    public class ManaExtractor : TowerAction
    {
        [SerializeField] private ValueProvider _manaIncrease;
        [SerializeField] private ValueProvider _interval;

        private TowerLevels _towerLevels;
        
        public ManaMechanics ManaMechanics;

        protected override void Init()
        {
            _towerLevels = GetComponent<TowerLevels>();
            _actionInterval = _interval.GetValue(_towerLevels.Levels);
        }

        protected override void TakeAction()
        {
            ManaMechanics.ChangeMana((int) _manaIncrease.GetValue(_towerLevels.Levels));
        }
    }
}