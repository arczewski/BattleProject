using AFSInterview.Abilities;
using UnityEngine;

namespace AFSInterview
{
    public class ArmorAttribute : UnitAttribute
    {
        [SerializeField] private int armor; // would be probably a slot for an item instead
        public int Armor => armor;
    }
}