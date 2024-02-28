using System;
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
        
        [SerializeField] private int currentUnitIndex = -1;
        [SerializeField] private Unit currentUnit;
        private bool battleStarted = false;
        private string winner = string.Empty;
        
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
            Debug.Log($"Unit {unit.gameObject.name} died!");
        }

        public void OnUnitTurnEnded(Unit unit)
        {
            if (currentUnit != unit)
                return;

            NextTurn();
        }

        [InspectorButton]
        public void StartBattle()
        {
            if (battleStarted)
                return;
            battleStarted = true;
            NextTurn();
        }
        
        private void NextTurn()
        {
            // linq is not the best idea due to allocation but I'm taking shortcuts due to prototype nature of the project ;)
            // and it is not called frequently
            var armiesInGame = unitsInBattle
                .Where(x => !x.IsDead)
                .Select(x => x.GetComponent<ArmyAttribute>().ArmyName)
                .Distinct()
                .ToList();
            
            if (armiesInGame.Count() == 1)
            {
                winner = armiesInGame.Single();
                Debug.Log($"Battle ended with {winner} winning!");
                return;
            }
            
            currentUnitIndex++;
            if (currentUnitIndex >= unitsInBattle.Count)
                currentUnitIndex = 0;
            currentUnit = unitsInBattle[currentUnitIndex];
            if (currentUnit.IsDead)
            {
                NextTurn();
                return;
            }

            currentUnit.TurnAcquired();
        }

        // Old way of doing things for gui but fast if we want to prototype a button
        // Alternatively there should be button StartBattle on this monobehaviour in unity editor
        public void OnGUI()
        {
            if (!battleStarted)
            {
                if (GUI.Button(new Rect((Screen.width / 2) - 50, Screen.height / 2, 100, 30), "StartBattle"))
                {
                    StartBattle();
                }
            }

            if (!string.IsNullOrEmpty(winner))
            {
                GUI.Label(new Rect((Screen.width / 2) - 100, Screen.height / 2, 2300, 40), $"Battle won by {winner}");
            }
        }
    }
}