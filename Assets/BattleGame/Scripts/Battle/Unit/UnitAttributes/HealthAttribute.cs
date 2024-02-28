using System;
using AFSInterview.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview
{
    public class HealthAttribute : UnitAttribute
    {
        [SerializeField] private int maxHealth; // would be probably a slot for an item instead
        [SerializeField] private int health; 
        public int Health => health;
        public int MaxHealth => health;

        public HealthChangedEvent OnHealthIncreased;
        public HealthChangedEvent OnHealthReduced;

        private void Start()
        {
            health = maxHealth;
        }

        public void UpdateBy(int value)
        {
            if (value == 0)
                return;
            
            health += value;
            health = Math.Max(health, 0);
            
            if(value > 0)
                OnHealthIncreased?.Invoke(health, value);
            else
                OnHealthReduced?.Invoke(health, value);
        }
    }

    [System.Serializable]
    public class HealthChangedEvent : UnityEvent<int, int>
    {
        
    }
}