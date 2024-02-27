using UnityEngine;

namespace AFSInterview.Abilities
{
    public interface IUnitAbility
    {
       bool IsReady();
    }

    public interface ITurnBasedAbility : IUnitAbility
    {
        void TurnAcquired();
    }

    public abstract class UnitAbility : MonoBehaviour, IUnitAbility
    {
        public abstract bool IsReady();
    }
}