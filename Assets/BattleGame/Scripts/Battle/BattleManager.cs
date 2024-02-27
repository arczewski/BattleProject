using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace AFSInterview
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private Transform battleGround;

        [SerializeField] private List<Unit> unitsInBattle = new();
        [SerializeField] private List<Unit> deadUnits = new();
        
        [SerializeField] private int currentUnitIndex = -1;
        [SerializeField] private Unit currentUnit;
        
        private void Start()
        {
            var units = battleGround.GetComponentsInChildren<Unit>().ToList();
            int unitsCount = units.Count;
            for(int i = 0; i < unitsCount; i++)
            {
                int randomUnitIndex = UnityEngine.Random.Range(0, units.Count);
                var unit = units[randomUnitIndex];
                unitsInBattle.Add(unit);
                units.RemoveAt(randomUnitIndex);
                unit.OnUnitTurnEnded.AddListener(OnUnitTurnEnded);
                unit.OnUnitDied.AddListener(OnUnitDied);
            }
            
        }

        public void OnUnitDied(Unit unit)
        {
            unitsInBattle.Remove(unit);
            deadUnits.Add(unit);
        }

        public void OnUnitTurnEnded(Unit unit)
        {
            if (currentUnit != unit)
                return;

            NextTurn();
        }

        [InspectorButton]
        public void NextTurn()
        {
            if (unitsInBattle.Count == 0)
            {
                Debug.Log("Battle ended with a draw");
                return;
            }

            // linq is not the best idea due to allocation but I'm taking shortcuts due to prototype nature of the project ;)
            // and it is not called frequently
            var armiesInGame = unitsInBattle
                .Select(x => x.GetComponent<ArmyAttribute>().ArmyName)
                .Distinct()
                .ToList();
            
            if (armiesInGame.Count() == 1)
            {
                Debug.Log($"Battle ended with {armiesInGame.Single()} winning!");
                return;
            }
            
            currentUnitIndex++;
            if (currentUnitIndex >= unitsInBattle.Count)
                currentUnitIndex = 0;
            currentUnit = unitsInBattle[currentUnitIndex];
            currentUnit.TurnAcquired();
        }
    }
}