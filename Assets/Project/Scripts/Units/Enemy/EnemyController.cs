using System.Threading.Tasks;
using System.Timers;
using Assets.Project.Scripts.Units.Fractions;
using UnityEngine;

namespace Scripts.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        protected EFraction EnemyFraction;
        protected float MoveSpeed;
        protected float RotationSpeed;
        protected float CooldownAttack;
        protected float DistanceAttack;
        protected int Damage;
        protected int Health;

        protected bool IsInAttackRange;
        protected bool AttackIsReady;
        
        protected GameObject Target;
        protected Rigidbody Rigidbody;

        private void Update()
        {
            float distanceToTarget = Vector3.Distance(transform.position,
                Target.transform.position);

            if (distanceToTarget <= DistanceAttack)
            {
                IsInAttackRange = true;
            }
            else
            {
                IsInAttackRange = false;
            }
        }
        
        public void FixedUpdate()
        {
            if (!IsInAttackRange)
            {
                Move();
            }
            else
            {
                Rigidbody.velocity = Vector3.zero;

                if (AttackIsReady)
                {
                    Attack();
                    AttackIsReady = false;
                    StartAttackCooldown();
                }
            }
            
            RotateTowardsTarget();
        }
        
        protected void MoveTowardTarget()
        {
            if (Target == null)
            {
                Debug.Log(this.gameObject.name + " on target");
                return;
            }
            
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            Rigidbody.velocity = direction * MoveSpeed;
        }

        protected void RotateTowardsTarget()
        {
            Vector3 lookDirection = (Target.transform.position - transform.position).normalized;

            lookDirection.y = 0;
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    targetRotation,
                    RotationSpeed * Time.deltaTime
                    );
            }
        }
        
        protected async void StartAttackCooldown()
        {
            var timer = 0f;

            while (timer < CooldownAttack)
            {
                timer += Time.deltaTime;
                //Debug.Log("timer = " + timer);
                await Task.Yield();
            }

            AttackIsReady = true;
        }

        protected abstract void Move();
        protected abstract void Attack();

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Death();
            }
        }

        public int DealDamage()
        {
            return Damage;
        }
        
        public void UpdateTarget(GameObject newTarget)
        {
            Target = newTarget;
        }

        private void Death()
        {
            Destroy(this);
        }
    }
}