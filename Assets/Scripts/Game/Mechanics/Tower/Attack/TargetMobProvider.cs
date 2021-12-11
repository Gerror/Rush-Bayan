using System.Collections;
using System.Collections.Generic;
using Game.Mechanics.Mob;
using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    public abstract class TargetMobProvider: MonoBehaviour
    {
        protected Tower _tower;
        protected MobSpawnMechanics _mobSpawnMechanics;

        protected void Awake()
        {
            _tower = GetComponent<Tower>();
            _tower.InitEvent += Init;
        }

        protected void Init()
        {
            _mobSpawnMechanics = _tower.MobSpawnMechanics;
        }
        
        public abstract GameObject GetTargetMob();
    }
}