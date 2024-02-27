using UnityEngine;

namespace AFSInterview.Abilities
{
    public class TargetUnitAbility : UnitAbility
    {
        [SerializeField] private Unit target;
        public Unit Target => target;

        public override bool IsReady()
        {
            return true;
        }
        
        public void SetTarget(Unit newTarget)
        {
            target = newTarget;
        }
    }
}