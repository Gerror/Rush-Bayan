using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private int _damage;
        [SerializeField] private GameObject _target;

        public int Damage
        {
            get => _damage;
        }

        public GameObject Target
        {
            get => _target;
        }

        public void Init(GameObject target, int damage)
        {
            _damage = damage;
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

        public virtual void OnExplosion() {}
    }
}
