using System;
using System.Collections;
using UnityEngine;
using Game.Mechanics.Tower;

namespace Game.Mechanics
{
    public class AiMechanics : MonoBehaviour
    {
        private TowerSpawnMechanics _towerSpawnMechanics;
        private ManaMechanics _manaMechanics;
        
        private void OnValidate()
        {
            if (!_towerSpawnMechanics)
                _towerSpawnMechanics = GetComponent<TowerSpawnMechanics>();
            if (!_manaMechanics)
                _manaMechanics = GetComponent<ManaMechanics>();
        }

        private void Start()
        {
            _manaMechanics.changeManaToEvent += ManaMenegment;
            
            StartCoroutine(StartingActions());
        }

        private IEnumerator StartingActions()
        {
            while (_manaMechanics.CurrentMana >= _towerSpawnMechanics.CurrentTowerPrice)
            {
                yield return new WaitForSeconds(0.2f);
                _towerSpawnMechanics.SpawnTower();
            }
        }

        private void ManaMenegment(int changeManaTo)
        {
            if (_manaMechanics.CurrentMana >= _towerSpawnMechanics.CurrentTowerPrice && changeManaTo > 0)
            {
                _towerSpawnMechanics.SpawnTower();
            }
        }
    }
}