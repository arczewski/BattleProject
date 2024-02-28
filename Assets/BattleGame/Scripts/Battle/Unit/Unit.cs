using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AFSInterview.Abilities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AFSInterview
{
    public interface IUnitAttributeManager
    {
        T GetAttribute<T>() where T : IUnitAttribute;
    }

    public interface IUnitAbilityManager
    {
        T GetAbility<T>() where T : IUnitAbility;
    }
    
    public class Unit : MonoBehaviour
    {
        // Observer pattern
        // decoupling classes/objects from each other
        // one to many relationship
        // Allows systems to fire events that would trigger some reactions without knowing anything about those external reactors
        public UnitTurnStarted OnUnitTurnStarted;
        public UnitTurnEnded OnUnitTurnEnded;
        public UnitDied OnUnitDied;
        public bool IsDead => isDead;

        [SerializeField] private UnitAttribute[] attributes;
        [SerializeField] private UnitAbility[] abilities;
        [SerializeField] private UnitStateMachine stateMachine;
        [SerializeField] private bool isDead;
        private bool activeTurn = false;

        
        public void TurnAcquired()
        {
            activeTurn = true;
            foreach (var ability in abilities)
            {
                if(ability is ITurnBasedAbility turnBasedAbility)
                    turnBasedAbility.TurnAcquired();
            }
            OnUnitTurnStarted?.Invoke(this);
            stateMachine.TransitionTo<UnitTargetingState>();
        }
        
        public void OnStateChanged(UnitState state)
        {
            if (state is UnitIdleState && activeTurn)
            {
                activeTurn = false;
                OnUnitTurnEnded?.Invoke(this);
            }

            if (state is UnitDeadState)
            {
                isDead = true;
                OnUnitDied?.Invoke(this);
            }
        }
    }

    [System.Serializable]
    public class UnitTurnEnded : UnityEvent<Unit>
    {
        
    }
    
    [System.Serializable]
    public class UnitTurnStarted : UnityEvent<Unit>
    {
        
    }
    
    [System.Serializable]
    public class UnitDied : UnityEvent<Unit>
    {
        
    }

    public enum Team
    {
        
    }
}

