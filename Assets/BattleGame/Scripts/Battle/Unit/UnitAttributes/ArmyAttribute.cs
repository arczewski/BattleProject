using AFSInterview.Abilities;
using UnityEngine;

namespace AFSInterview
{
    public class ArmyAttribute : UnitAttribute
    {
        [SerializeField] private string armyName;
        public string ArmyName => armyName;
    }
}