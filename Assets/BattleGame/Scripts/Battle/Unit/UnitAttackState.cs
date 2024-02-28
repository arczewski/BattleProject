using System;
using AFSInterview.Abilities;
using UnityEngine;

namespace AFSInterview
{
    public class UnitAttackState : UnitState
    {
        [SerializeField] private DistanceAttackAbility distanceAttackAbility;
        [SerializeField] private TargetUnitAbility targetUnitAbility;
        

        private void OnEnable()
        {
            if (!distanceAttackAbility.IsReady())
            {
                stateMachine.TransitionTo<UnitIdleState>();
                return;
            }

            if (targetUnitAbility.Target == null)
            {
                stateMachine.TransitionTo<UnitIdleState>();
                return;
            }

            distanceAttackAbility.AttackCompleted.AddListener(OnAttackCompleted);
            var targetHealthAttribute = targetUnitAbility.Target.GetComponent<HealthAttribute>();
            var targetArmorAttribute = targetUnitAbility.Target.GetComponent<ArmorAttribute>();
            var unitTraitsAttribute = targetUnitAbility.Target.GetComponent<UnitTraitsAttribute>();
            
            Debug.Log($"Attacking {targetUnitAbility.Target.gameObject.name}");
            distanceAttackAbility.InitiateAttack(targetUnitAbility.Target.transform.position, targetHealthAttribute, targetArmorAttribute, unitTraitsAttribute);
        }

        private void OnAttackCompleted()
        {
            distanceAttackAbility.AttackCompleted.RemoveListener(OnAttackCompleted);
            stateMachine.TransitionTo<UnitIdleState>();
        }
    }
}