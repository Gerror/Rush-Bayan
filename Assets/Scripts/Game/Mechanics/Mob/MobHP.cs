using System;
using Game.UI;
using Game.Mechanics.Tower.Attack;
using UnityEngine;

namespace Game.Mechanics.Mob
{
    [RequireComponent(typeof(Collider2D))]
    public class MobHP : MonoBehaviour
    {
        [SerializeField] private MobHpLabel mobHpLabel;
        private int _startHp;
        private int _currentHp;
        
        public event Action ChangeHpEvent;
        public event Action DeadEvent;

        public void Init(int startHp)
        {
            _startHp = startHp;
            _currentHp = startHp;
            mobHpLabel.SetHp(_currentHp);
        }

        public void ChangeHp(int changeHpTo)
        {
            _currentHp += changeHpTo;
            if (_currentHp <= 0)
            {
                DeadEvent?.Invoke();
                return;
            }
            
            mobHpLabel.SetHp(_currentHp);
            ChangeHpEvent?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                Bullet bullet = other.gameObject.GetComponent<Bullet>();
                if (bullet.Target == gameObject)
                {
                    ChangeHp(-1 * bullet.Damage);
                    bullet.OnExplosion();
                    Destroy(other.gameObject);
                }
            }
        }
    }
}