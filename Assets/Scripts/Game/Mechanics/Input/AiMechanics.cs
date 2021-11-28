using System.Collections;
using UnityEngine;

namespace Game.Mechanics.Input
{
    public class AiMechanics : InputMechanics
    {
        public override void StartInputMechanics()
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