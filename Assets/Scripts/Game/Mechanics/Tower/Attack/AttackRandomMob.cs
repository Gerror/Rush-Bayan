using System;
using System.Collections;
using Game.Mechanics.Mob;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(TowerAttackMechanics))]
    public class AttackRandomMob : MonoBehaviour, ITowerAttack
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
            return (GameObject) _mobSpawnMechanics.MobOrderedDictionary[
                Random.Range(0, _mobSpawnMechanics.MobOrderedDictionary.Count)];
        }
    }
}