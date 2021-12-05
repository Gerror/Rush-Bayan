using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Mechanics.Tower.Attack
{
    public class PoisonBullet : Bullet
    {
        [SerializeField] private GameObject _poissonCloud;
        [SerializeField] private float _damagePeriod = 0.5f;
        [SerializeField] private float _cloudLifetime = 3f;

        [SerializeField] private Transform _tempObjectParent;

        private PrefabFactory _prefabFactory;
        
        [Inject]
        private void Construct(PrefabFactory prefabFactory, Transform tempObjectParent)
        {
            _prefabFactory = prefabFactory;
            _tempObjectParent = tempObjectParent;
        }

        public override void DestroyBullet()
        {
            OnExplosion();
            Destroy(gameObject);
        }

        private void OnExplosion()
        {
            GameObject poissonCloudGo = _prefabFactory.Spawn(_poissonCloud, 
                transform.position, Quaternion.identity, _tempObjectParent);
            PoisonCloud poisonCloud = poissonCloudGo.GetComponent<PoisonCloud>();
            if (poisonCloud)
            {
                poisonCloud.Init(Damage, _damagePeriod, _cloudLifetime);
            }
        }
    }
}