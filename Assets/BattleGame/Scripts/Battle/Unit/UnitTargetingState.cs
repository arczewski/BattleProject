using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AFSInterview.Abilities;
using UnityEngine;

namespace AFSInterview
{
    public class UnitTargetingState : UnitState
    {
        [SerializeField] private int visibleRange = 1000; // should be attribute
        [SerializeField] private ArmyAttribute armyAttribute;
        [SerializeField] private TargetUnitAbility targetUnitAbility;
        [SerializeField] private float lookAtSpeed = 10;
        
        private List<Unit> cachedEnemies = new List<Unit>();
        private int unitLayerMask;
        
        private void OnEnable()
        {
            unitLayerMask = LayerMask.GetMask("Unit");
            cachedEnemies.Clear();
            var colliders = Physics.OverlapSphere(transform.position, visibleRange, unitLayerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                var unitObject = colliders[i].gameObject;
                var unit = unitObject.GetComponent<Unit>();
                var enemyArmyAttribute = unit.GetComponent<ArmyAttribute>();
                if (enemyArmyAttribute.ArmyName == armyAttribute.ArmyName)
                {
                    // same army keep searching
                    continue;
                }
                
                var enemyHealthAttribute = unit.GetComponent<HealthAttribute>();
                if (enemyHealthAttribute.Health <= 0)
                {
                    continue;
                }
                cachedEnemies.Add(unit);
            }
            
            /*UnitAI closestEnemy;
            float minDistance = float.MaxValue;
            Vector3 currentPosition = transform.position;
            foreach (var enemy in cachedEnemies)
            {
                var distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);
                if (distanceToEnemy < minDistance)
                {
                    minDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }*/
            
            if (cachedEnemies.Count == 0)
            {
                stateMachine.TransitionTo<UnitIdleState>();
                return;
            }
            
            var randomEnemy = cachedEnemies[UnityEngine.Random.Range(0, cachedEnemies.Count)];
            targetUnitAbility.SetTarget(randomEnemy);
            StartCoroutine(RotateTowardsTarget(randomEnemy));
        }

        private IEnumerator RotateTowardsTarget(Unit targetUnit)
        {
            var target = targetUnit.transform;
            while(Vector3.Angle(transform.forward, target.position - transform.position) > 0.1f) {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lookAtSpeed * Time.deltaTime);

                yield return null;
            }
            stateMachine.TransitionTo<UnitAttackState>();
        }
    }
}