using System;
using AFSInterview.Abilities;
using UnityEngine;

namespace AFSInterview
{
    public class UnitAttackState : UnitState
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AttackAbility attackAbility;
        [SerializeField] private TargetUnitAbility targetUnitAbility;
        

        private void OnEnable()
        {
            if (!attackAbility.IsReady())
            {
                stateMachine.TransitionTo<UnitIdleState>();
                return;
            }

            if (targetUnitAbility.Target == null)
            {
                stateMachine.TransitionTo<UnitIdleState>();
                return;
            }

            var targetHealthAttribute = targetUnitAbility.Target.GetComponent<HealthAttribute>();
            var targetArmorAttribute = targetUnitAbility.Target.GetComponent<ArmorAttribute>();
            var unitTraitsAttribute = targetUnitAbility.Target.GetComponent<UnitTraitsAttribute>();

            Debug.Log($"Attacking {targetUnitAbility.Target.gameObject.name}");
            attackAbility.Attack(targetHealthAttribute, targetArmorAttribute, unitTraitsAttribute);
            animator.SetTrigger("Attack");
        }
    }
}