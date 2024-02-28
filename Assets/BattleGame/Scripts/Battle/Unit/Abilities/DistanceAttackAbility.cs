using System;
using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview.Abilities
{
    public class DistanceAttackAbility : UnitAbility, ITurnBasedAbility
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform handTransform;
        [SerializeField] private int waitBetweenTurns;
        [SerializeField] private int currentTurnCount;
        [SerializeField] private int weaponDamage;
        [SerializeField] private UnitTraits doubleEffectveAgainstTraits;
        
        private static readonly int AttackAnimation = Animator.StringToHash("Attack");
        public int WeaponDamage => weaponDamage;
        
        private int calculatedProjectileDamage;
        private HealthAttribute cachedTargetHealthAttribute;
        private Vector3 targetWorldPosition;

        public UnityEvent AttackCompleted;

        private void Start()
        {
            currentTurnCount = waitBetweenTurns;
        }

        public override bool IsReady()
        {
            return currentTurnCount >= waitBetweenTurns;
        }

        public void TurnAcquired()
        {
            currentTurnCount++;
        }

        public void InitiateAttack(Vector3 worldPosition, HealthAttribute enemyHealth, ArmorAttribute enemyArmor, UnitTraitsAttribute unitTraitsAttribute)
        {
            targetWorldPosition = worldPosition;
            cachedTargetHealthAttribute = enemyHealth;
            calculatedProjectileDamage = weaponDamage;
            if ((doubleEffectveAgainstTraits & unitTraitsAttribute.Traits) != 0)
                calculatedProjectileDamage *= 2;
            calculatedProjectileDamage = Math.Max(1, calculatedProjectileDamage - enemyArmor.Armor); //ensure at least 1hp dmg
            animator.SetTrigger(AttackAnimation);
            currentTurnCount = 0;
        }
        
        public void OnAttackAnimationComplete()
        {
            var projectileObject = ObjectPool.Instance.GetOrCreate(ObjectType.Arrow);
            projectileObject.transform.position = handTransform.position;
            var projectile = projectileObject.GetComponent<Projectile>();
            projectile.OnTargetHit.AddListener(OnTargetHit);
            projectile.SetTarget(targetWorldPosition);
        }

        private void OnTargetHit()
        {
            UnityEngine.Debug.Log($"Dealing {calculatedProjectileDamage} damage");
            cachedTargetHealthAttribute.UpdateBy(-calculatedProjectileDamage);
            AttackCompleted?.Invoke();
        }
    }
}