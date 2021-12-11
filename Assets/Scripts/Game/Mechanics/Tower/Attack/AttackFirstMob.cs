using Game.Mechanics.Mob;
using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(TowerAttackMechanics))]
    public class AttackFirstMob : MonoBehaviour, ITowerAttack
    {
        private TowerAttackMechanics _towerAttackMechanics;
        private MobSpawnMechanics _mobSpawnMechanics;
        
        private void Start()
        {
            _towerAttackMechanics = GetComponent<TowerAttackMechanics>();
            _mobSpawnMechanics = _towerAttackMechanics.MobSpawnMechanics;
        }
        
        public GameObject GetTargetMob()
        {
            return (GameObject) _mobSpawnMechanics.MobOrderedDictionary[0];
        }
    }
}
