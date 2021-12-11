using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    public class VampiricBullet : Bullet
    {
        [SerializeField] private float _interval = 7.9f;
        [SerializeField] private ValueProvider _manaIncrease;
        
        public override void DestroyBullet()
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(OnExplosion());
        }
        
        private IEnumerator OnExplosion()
        {
            while (_target)
            {
                _tower.ManaMechanics.ChangeMana((int) _manaIncrease.GetValue(_tower.TowerLevels.Levels));
                yield return new WaitForSeconds(_interval);
            }
            
            Destroy(gameObject);
        }
    }
}