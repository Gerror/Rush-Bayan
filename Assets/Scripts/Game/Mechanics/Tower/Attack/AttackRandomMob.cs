using System;
using System.Collections;
using Game.Mechanics.Mob;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(TowerAttackMechanics))]
    public class AttackRandomMob : TargetMobProvider
    {
        public override GameObject GetTargetMob()
        {
            return (GameObject) _mobSpawnMechanics.MobOrderedDictionary[
                Random.Range(0, _mobSpawnMechanics.MobOrderedDictionary.Count)];
        }
    }
}