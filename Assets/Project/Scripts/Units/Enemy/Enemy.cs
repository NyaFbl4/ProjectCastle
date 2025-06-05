using Assets.Project.Scripts.Units.Fractions;
using UnityEngine;

namespace Scripts.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        protected EFraction EnemyFraction;
        protected float MoveSpeed;
        protected float RotationSpeed;
        protected int Damage;
        protected int Health;

        protected float DistanceAttack;

        protected GameObject Target;
        protected Rigidbody Rigidbody;

        public void UpdateTarget(GameObject newTarget)
        {
            Target = newTarget;
        }

        protected void MoveTowardTarget()
        {
            if (Target == null)
            {
                Debug.Log(this.gameObject.name + " on target");
                return;
            }
            else
            {
                Vector3 direction = (Target.transform.position - transform.position).normalized;

                Rigidbody.velocity = direction * MoveSpeed; 
            }

            //transform.position += vector3 * MoveSpeed;
        }

        protected void FollowTarget()
        {
            
        }

        public abstract void Move();

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Death();
            }
        }

        public virtual int DealDamage()
        {
            return Damage;
        }

        private void Death()
        {
            Destroy(this);
        }
    }
}