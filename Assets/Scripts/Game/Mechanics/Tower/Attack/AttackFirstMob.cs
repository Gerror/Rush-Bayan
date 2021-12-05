using Game.Mechanics.Mob;
using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(TowerAttackMechanics))]
    public class AttackFirstMob : TargetMobProvider
    {
        public override GameObject GetTargetMob()
        {
            return (GameObject) _mobSpawnMechanics.MobOrderedDictionary[0];
        }
    }
}
