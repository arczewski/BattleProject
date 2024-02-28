using UnityEngine;

namespace AFSInterview
{
    public class UnitDeadState : UnitState
    {
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            _animator.SetTrigger("Dead");
        }
    }
}