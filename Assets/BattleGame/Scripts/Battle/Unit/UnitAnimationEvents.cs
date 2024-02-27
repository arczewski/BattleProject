using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview
{
    public class UnitAnimationEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnAttackAnimationCompleted;
        [SerializeField] private UnityEvent OnDyingAnimationCompleted;
        
        public void AttackAnimationCompleted()
        {
            OnAttackAnimationCompleted?.Invoke();
        }

        public void DyingAnimationCompleted()
        {
            OnDyingAnimationCompleted?.Invoke();
        }
    }
}
