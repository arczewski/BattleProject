using AFSInterview.Abilities;
using Unity.Collections;
using UnityEngine;

namespace AFSInterview
{
    public abstract class UnitState : MonoBehaviour
    {
        protected UnitStateMachine stateMachine { get; private set; }
        private void Awake()
        {
            stateMachine = GetComponent<UnitStateMachine>();
        }
    }
}