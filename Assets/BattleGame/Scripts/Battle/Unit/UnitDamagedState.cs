using System;
using UnityEngine;

namespace AFSInterview
{
    public class UnitDamagedState : UnitState
    {
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            //_animator.SetTrigger("Damaged");
        }
    }
}