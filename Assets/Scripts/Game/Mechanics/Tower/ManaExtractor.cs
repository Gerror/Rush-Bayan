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
        private ManaMechanics _manaMechanics;

        protected override void Init()
        {
            _manaMechanics = _tower.ManaMechanics;
            _towerLevels = GetComponent<TowerLevels>();
            _actionInterval = _interval.GetValue(_towerLevels.Levels);
        }

        protected override void TakeAction()
        {
            _manaMechanics.ChangeMana((int) _manaIncrease.GetValue(_towerLevels.Levels));
        }
    }
}