using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private ValueProvider _damage;
        protected Tower _tower;
        [SerializeField] protected GameObject _target;

        public int Damage
        {
            get => (int) _damage.GetValue(_tower.TowerLevels.Levels);
        }

        public GameObject Target
        {
            get => _target;
        }

        public void Init(GameObject target, ValueProvider damage, Tower tower)
        {
            _damage = damage;
            _tower = tower;
            _target = target;
        }

        private void Update()
        {
            if (_target)
            {
                Vector3 direction = _target.transform.position - transform.position;
                direction = direction.normalized * _speed;
                transform.Translate(direction * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public virtual void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}
