using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Mechanics.Tower.Attack
{
    public class PoisonBullet : Bullet
    {
        [SerializeField] private GameObject _poissonCloud;
        [SerializeField] private float _damagePeriod = 0.5f;
        [SerializeField] private float _cloudLifetime = 3f;

        public override void OnExplosion()
        {
            GameObject poissonCloudGo = Object.Instantiate(_poissonCloud, 
                transform.position, Quaternion.identity);
            PoisonCloud poisonCloud = poissonCloudGo.GetComponent<PoisonCloud>();
            if (poisonCloud)
            {
                poisonCloud.Init(Damage, _damagePeriod, _cloudLifetime);
            }
        }
    }
}