using Assets.Project.Scripts.Units.Fractions;
using UnityEngine;

namespace Scripts.Enemy.TypeEnemies
{
    public class MiliEnemy : EnemyController
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private EFraction _enemyFraction;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _distanceAttack;
        [SerializeField] private float _cooldownAttack;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;

        //[SerializeField] private bool _isInAttackRange;

        [SerializeField] private Rigidbody _rigidbody;
        
        public void Start()
        {
            //_rigidbody = GetComponent<Rigidbody>();
            
            base.Target = _target;
            base.EnemyFraction = _enemyFraction;
            base.MoveSpeed = _moveSpeed;
            base.RotationSpeed = _rotationSpeed;
            base.CooldownAttack = _cooldownAttack;
            base.Damage = _damage;
            base.Health = _health;
            base.Rigidbody = _rigidbody;
            base.DistanceAttack = _distanceAttack;
            base.IsInAttackRange = false;

            base.AttackIsReady = true;

            //_isInAttackRange = false;
        }
        

        protected override void Move()
        {
            MoveTowardTarget();
        }

        protected override void Attack()
        {
            Debug.Log("Attack");
        }
    }
}