using System;
using AFSInterview.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview
{
    public class HealthAttribute : UnitAttribute
    {
        [SerializeField] private int health; // would be probably a slot for an item instead
        public int Health => health;
        public HealthChangedEvent OnHealthIncreased;
        public HealthChangedEvent OnHealthReduced;

        public void UpdateBy(int value)
        {
            if (value == 0)
                return;
            
            health += value;
            health = Math.Max(health, 0);
            
            if(value > 0)
                OnHealthIncreased?.Invoke(health);
            else
                OnHealthReduced?.Invoke(health);
        }
    }

    [System.Serializable]
    public class HealthChangedEvent : UnityEvent<int>
    {
        
    }
}