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
        None = 0,
        Light = 2, 
        Armored = 4, 
        Mechanical = 8
    }
}
