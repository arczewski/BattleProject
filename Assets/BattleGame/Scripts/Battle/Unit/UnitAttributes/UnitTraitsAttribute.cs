using System;
using System.Collections;
using System.Collections.Generic;
using AFSInterview.Abilities;
using UnityEngine;

namespace AFSInterview
{
    public class UnitTraitsAttribute : UnitAttribute
    {
        [SerializeField] private UnitTraits traits;
        public UnitTraits Traits => traits;
    }

    [Flags]
    public enum UnitTraits
    {
        Light, 
        Armored, 
        Mechanical
    }
}
