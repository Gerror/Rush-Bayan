using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mechanics.Tower
{
    [RequireComponent(typeof(TowerLevels))]
    public class ManaExtractor : MonoBehaviour
    {
        [SerializeField] private ValueProvider _manaIncrease;
        [SerializeField] private ValueProvider _interval;

        private TowerLevels _towerLevels;
        
        public ManaMechanics ManaMechanics;

        private void Start()
        {
            _towerLevels = GetComponent<TowerLevels>();
            StartCoroutine(StartExtract());
        }

        private IEnumerator StartExtract()
        {
            while (true)
            {
                yield return new WaitForSeconds(_interval.GetValue(_towerLevels.Levels));
                Extract();
            }
        }

        private void Extract()
        {
            ManaMechanics.ChangeMana((int) _manaIncrease.GetValue(_towerLevels.Levels));
        }
    }
}