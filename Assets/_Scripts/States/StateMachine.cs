using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.States
{
    public class StateMachine
    {
        private List<Transition> _transitions = new();
        public State CurrentState;
        public string Name;
        
        public StateMachine(string name)
        {
            Name = name;
        }
        
        public void AddTransition(State from, State to, Func<bool> predicate)
        {
            _transitions.Add(new Transition()
            {
                From = from,
                To = to,
                Predicate = predicate
            });
        }

        public void SetState(State state)
        {
            CurrentState?.OnExit();

            CurrentState = state;
            
            CurrentState.OnEnter();
        }
        
        public void Update()
        {
            if (CurrentState == null)
            {
                Debug.LogError("There is no state selected for State Machine: " + Name);
            }

            var allTransitions = _transitions.Where(x => x.From == CurrentState);
            var satisfyingTransition = allTransitions.FirstOrDefault(x => x.Predicate());

            if (satisfyingTransition == null) return;
            
            SetState(satisfyingTransition.To);
        }
    }
}