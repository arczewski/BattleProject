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
        public UnitTurnEnded OnUnitTurnEnded;
        public UnitDied OnUnitDied;

        [SerializeField] private UnitAttribute[] attributes;
        [SerializeField] private UnitAbility[] abilities;
        [SerializeField] private UnitStateMachine stateMachine;
        private bool activeTurn = false;
        
        public void TurnAcquired()
        {
            activeTurn = true;
            foreach (var ability in abilities)
            {
                if(ability is ITurnBasedAbility turnBasedAbility)
                    turnBasedAbility.TurnAcquired();
            }

            stateMachine.TransitionTo<UnitTargetingState>();
        }
        
        public void OnStateChanged(UnitState state)
        {
            if(state is UnitIdleState && activeTurn)
                OnUnitTurnEnded?.Invoke(this);
            
            if(state is UnitDeadState)
                OnUnitDied?.Invoke(this);
        }
    }

    [System.Serializable]
    public class UnitTurnEnded : UnityEvent<Unit>
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

