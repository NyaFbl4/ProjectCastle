using Assets.Project.Scripts.Units.Fractions;
using UnityEngine;

namespace Scripts.Enemy.TypeEnemies
{
    public class MiliEnemy : EnemyController
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private EFraction _enemyFraction;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _distanceAttack;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;

        [SerializeField] private Rigidbody _rigidbody;
        
        public void Start()
        {
            //_rigidbody = GetComponent<Rigidbody>();
            
            base.Target = _target;
            base.EnemyFraction = _enemyFraction;
            base.MoveSpeed = _moveSpeed;
            base.Damage = _damage;
            base.Health = _health;
            base.Rigidbody = _rigidbody;
        }

        public void FixedUpdate()
        {
            Move();
        }

        public override void Move()
        {
            MoveTowardTarget();
        }
    }
}