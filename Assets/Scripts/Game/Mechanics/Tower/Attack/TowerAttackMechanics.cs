using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Game.Mechanics.Mob;

namespace Game.Mechanics.Tower.Attack
{
    public class TowerAttackMechanics : MonoBehaviour
    {
        [Min(100)] [SerializeField] private int _damage;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _attackInterval;

        private ITowerAttack _towerAttack;
        public MobSpawnMechanics MobSpawnMechanics;

        private IEnumerator Attack()
        {
            if (MobSpawnMechanics.MobOrderedDictionary.Count > 0)
            {
                AttackMob(_towerAttack.GetTargetMob());
            }

            while (true)
            {
                yield return new WaitForSeconds(_attackInterval);
                if (MobSpawnMechanics.MobOrderedDictionary.Count > 0)
                {
                    AttackMob(_towerAttack.GetTargetMob());
                }
            }
        }

        private void AttackMob(GameObject mob)
        {
            if (!mob)
                return;

            GameObject bulletGO = Object.Instantiate(_bulletPrefab, transform);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Init(mob, _damage);
        }

        private void Start()
        {
            _towerAttack = GetComponent<ITowerAttack>();
            StartCoroutine(Attack());
        }
    }
}