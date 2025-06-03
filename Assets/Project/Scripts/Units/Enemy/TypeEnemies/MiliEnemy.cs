using Assets.Project.Scripts.Units.Fractions;
using UnityEngine;

namespace Enemy.TypeEnemies
{
    public class MiliEnemy : Enemy
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private EFraction _enemyFraction;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _distanceAttack;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;

        public void Init()
        {
            base.Target = _target;
            base.EnemyFraction = _enemyFraction;
            base.MoveSpeed = _moveSpeed;
            base.Damage = _damage;
            base.Health = _health;
        }

        public override void Move()
        {
            FollowTarget();
        }
    }
}