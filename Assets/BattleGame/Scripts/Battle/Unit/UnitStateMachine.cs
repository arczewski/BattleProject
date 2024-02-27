using System.Collections.Generic;
using System.Linq;
using AFSInterview.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview
{
    public class UnitStateMachine : MonoBehaviour
    {
        private UnitState currentUnitState;
        private Dictionary<System.Type, UnitState> allStates = new();

        public UnitStateMachineStateChanged OnUnitStateMachineStateChanged;

        private void Awake()
        {
            allStates = GetComponents<UnitState>().ToDictionary(x => x.GetType(), y => y);
            foreach (var state in allStates)
            {
                state.Value.enabled = false;
            }
            TransitionTo<UnitIdleState>();
        }
        
        public void TransitionTo<T>() where T : UnitState
        {
            if (!allStates.TryGetValue(typeof(T), out var unitState))
                return;
            unitState.enabled = true;
            OnUnitStateMachineStateChanged.Invoke(unitState);
        }

        public void OnHealthReduced(int currentHealth)
        {
            if(currentHealth == 0)
                TransitionTo<UnitDeadState>();
            else
                TransitionTo<UnitDamagedState>();
        }
        
        
        public void OnAttackAnimationCompleted()
        {
            TransitionTo<UnitIdleState>();
        }

        public void OnDyingAnimationCompleted()
        {
            
        }
    }
    
    [System.Serializable]
    public class UnitStateMachineStateChanged : UnityEvent<UnitState>
    {
        
    }
}

