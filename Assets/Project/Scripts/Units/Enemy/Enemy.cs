using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Project.Scripts.Units.Fractions;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        protected EFraction EnemyFraction;
        protected float MoveSpeed;
        protected int Damage;
        protected int Health;

        protected float DistanceAttack;

        protected GameObject Target;

        public void UpdateTarget(GameObject newTarget)
        {
            Target = newTarget;
        }

        protected void MoveTowardTarget()
        {
            //Vector3 vector3 = (Target.transform.position - transform.position).normalized;
        }

        protected void FollowTarget()
        {
            if (Target != null)
            {
                Vector3 vector3 = (Target.transform.position - transform.position).normalized;

                transform.position += vector3 * MoveSpeed;
            }
            else
            {
                Debug.Log(this.gameObject.name + " on target");
            }
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