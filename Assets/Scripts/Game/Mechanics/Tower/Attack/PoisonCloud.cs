using System;
using System.Collections;
using System.Collections.Generic;
using Game.Mechanics.Mob;
using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PoisonCloud : MonoBehaviour
    {
        private Dictionary<Collider2D, MobHP> _mobsDictionary;
        private int _damage = 0;
        private float _damagePeriod = 0.5f;
        private float _cloudLifetime = 3f;

        private void Start()
        {
            _mobsDictionary = new Dictionary<Collider2D, MobHP>();
            StartCoroutine(PeriodicDamage());
            StartCoroutine(Lifetime());
        }

        public void Init(int damage, float damagePeriod, float cloudLifetime)
        {
            _damage = damage;
            _damagePeriod = damagePeriod;
            _cloudLifetime = cloudLifetime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            MobHP mobHp = other.GetComponent<MobHP>();
            if (mobHp && !_mobsDictionary.ContainsKey(other))
                _mobsDictionary.Add(other, mobHp);
        }

        private IEnumerator PeriodicDamage()
        {
            while (true)
            {
                yield return new WaitForSeconds(_damagePeriod);
                foreach (KeyValuePair<Collider2D, MobHP> keyValuePair in _mobsDictionary)
                {
                    if (keyValuePair.Value)
                        keyValuePair.Value.ChangeHp(-1 * _damage);
                }
            }
        }

        private IEnumerator Lifetime()
        {
            yield return new WaitForSeconds(_cloudLifetime);
            Destroy(gameObject);
        }
    }
}