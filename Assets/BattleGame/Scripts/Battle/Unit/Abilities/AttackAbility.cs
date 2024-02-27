using System;
using Unity.Collections;
using UnityEngine;

namespace AFSInterview.Abilities
{
    public class AttackAbility : UnitAbility, ITurnBasedAbility
    {
        [SerializeField] private int waitBetweenTurns;
        [SerializeField] private int currentTurnCount;
        [SerializeField] private int weaponDamage;
        [SerializeField] private UnitTraits doubleEffectveAgainstTraits;

        public int WeaponDamage => weaponDamage;

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

        public void Attack(HealthAttribute enemyHealth, ArmorAttribute enemyArmor, UnitTraitsAttribute unitTraitsAttribute)
        {
            int damage = weaponDamage;
            if ((doubleEffectveAgainstTraits & unitTraitsAttribute.Traits) != 0)
                damage *= 2;
            damage = Math.Max(1, damage - enemyArmor.Armor); //ensure at least 1hp dmg
            UnityEngine.Debug.Log($"Dealing {damage} damage");
            enemyHealth.UpdateBy(-damage);
        }
    }
}